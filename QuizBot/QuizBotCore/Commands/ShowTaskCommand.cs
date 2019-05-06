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

            await SendTask(task, chat, user, client, quizService, logger);
        }

        private async Task SendTask(TaskDTO task, Chat chat, UserEntity user, TelegramBotClient client,
            IQuizService quizService, ILogger logger)
        {
            var userProgress = quizService.GetCurrentProgress(user.Id, topicDto.Id, levelDto.Id);
            var progressBar = new CircleProgressBar();
            var progress = progressBar.GenerateProgressBar(userProgress.TasksSolved, userProgress.TasksCount);
            var question = task.Question;
            
            logger.LogInformation($"Прогресс: {userProgress.TasksSolved.ToString()}: {userProgress.TasksCount.ToString()}");
            var message = FormatMessage(question, progress);
            var controlButtons = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
            };
            
            if (task.HasHints)
                controlButtons.Append(InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint));
            
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task
                    .Answers
                    .Select(InlineKeyboardButton.WithCallbackData),
                controlButtons
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

            return $"{topicName}" +
                   $"{levelName}" +
                   $"{progress}\n" +
                   $"{questionFormatted}";
        }
    }
}