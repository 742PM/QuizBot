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
    internal class NextTaskCommand : ICommand
    {
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;

        public NextTaskCommand(TopicDTO topicDto, LevelDTO levelDto)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
        }
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository,
            ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var task = quizService.GetNextTaskInfo(user.Id);
            if (task == null)
            {
                await client.SendTextMessageAsync(chat.Id, DialogMessages.NextTaskNotAvailable);
                return;
            }

            var question = task.Question;
            
            var topicName = $"Тема: \"{topicDto.Name}\".\n";
            var levelName = $"Уровень: \"{levelDto.Description}\".\n";
            
            var questionFormatted = "```csharp\n" +
                                    $"{question}\n" +
                                    "```";
            
            var questionInMarkdown = $"{topicName}{levelName}{questionFormatted}";

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task.Answers.Select(InlineKeyboardButton.WithCallbackData),
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                    InlineKeyboardButton.WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint),
                }
            });

            await client.SendTextMessageAsync(chat.Id, questionInMarkdown, replyMarkup: keyboard,
                parseMode: ParseMode.Markdown);
        }
    }
}