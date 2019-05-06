using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class ShowTaskCommand : ICommand
    {
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;
        private readonly bool isNext;

        public ShowTaskCommand(TopicDTO topicDto, LevelDTO levelDto, bool isNext = false)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
            this.isNext = isNext;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            TaskDTO task = null;
            if (isNext)
            {
                task = quizService.GetNextTaskInfo(user.Id);
                if (task == null)
                {
                    await client.SendTextMessageAsync(chat.Id, DialogMessages.NextTaskNotAvailable);
                    return;
                }
            }
            else
            {
                task = quizService.GetTaskInfo(user.Id, topicDto.Id, levelDto.Id);
            }

            await SendTask(task, chat, user, client, quizService);
        }

        private async Task SendTask(TaskDTO task, Chat chat, UserEntity user, TelegramBotClient client, IQuizService quizService)
        {
            var percentage = quizService.GetCurrentProgress(user.Id, topicDto.Id, levelDto.Id);
            var progressBar = new CircleProgressBar();
            var progress = progressBar.GenerateProgressBar(double.Parse(percentage), 1, 10);
            var question = task.Question;
            
            var message = FormatMessage(question, progress);

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task
                    .Answers
                    .Select(InlineKeyboardButton.WithCallbackData),
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint),
                }
            });

            await client.SendTextMessageAsync(chat.Id, message, replyMarkup: keyboard,
                parseMode: ParseMode.Markdown);
        }

        private string FormatMessage(string question, string progressBar)
        {
            var topicName = $"{DialogMessages.TopicName} \"**{topicDto.Name}**\"\n";
            var levelName = $"{DialogMessages.LevelName} \"**{levelDto.Description}**\"\n";
            var progress = $"{DialogMessages.ProgressMessage} {progressBar}\n";

            var questionFormatted = "```csharp\n" +
                                    $"{question}\n" +
                                    "```";

            return $"{topicName}{levelName}{progress}{questionFormatted}";
        }
    }
}