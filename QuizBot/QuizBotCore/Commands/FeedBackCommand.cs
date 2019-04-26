using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class FeedBackCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var message = "Есть вопрос? Пиши нам!";
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithUrl("Артемий", "telegram.me/aizakov"),
                InlineKeyboardButton.WithUrl("Антон", "telegram.me/funfine"),
                InlineKeyboardButton.WithUrl("Василий", "telegram.me/vaspahomov"),
                InlineKeyboardButton.WithUrl("Роман", "telegram.me/romutchio")
            });
            await client.SendTextMessageAsync(chat.Id,message, replyMarkup:keyboard);
        }
    }
}