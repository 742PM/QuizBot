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
            var questionInMarkdown = "```csharp\n" +
                                     $"{question}\n" +
                                     "```";

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task.Answers.Select(InlineKeyboardButton.WithCallbackData),
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                    InlineKeyboardButton.WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint),
//                    InlineKeyboardButton.WithCallbackData(ButtonNames.NextTask, StringCallbacks.NextTask)
                }
            });

            await client.SendTextMessageAsync(chat.Id, questionInMarkdown, replyMarkup: keyboard,
                parseMode: ParseMode.Markdown);
        }
    }
}