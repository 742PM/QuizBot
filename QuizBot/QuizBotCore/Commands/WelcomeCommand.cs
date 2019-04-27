using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class WelcomeCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var chatId = chat.Id;
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Topics, StringCallbacks.Topics),
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Info, StringCallbacks.Info),
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Feedback, StringCallbacks.Feedback)
                }
            });
            await client.SendTextMessageAsync(chatId, DialogMessages.WelcomeMessage, replyMarkup: inlineKeyboard);
        }
    }
}