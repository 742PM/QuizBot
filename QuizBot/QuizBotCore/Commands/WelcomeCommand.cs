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
            var messageText = "Главное меню:";
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Порешать задачки", "topics"),
                    InlineKeyboardButton.WithCallbackData("Справка", "info"),
                    InlineKeyboardButton.WithCallbackData("Обратная связь", "feedback")
                }
            });
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: inlineKeyboard);
        }
    }
}