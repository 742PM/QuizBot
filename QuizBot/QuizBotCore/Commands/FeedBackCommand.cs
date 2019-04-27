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
            var keyboard = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithUrl(
                    DialogMessages.FeedbackContact.Item1, 
                    DialogMessages.FeedbackContact.Item2)
                );
            await client.SendTextMessageAsync(chat.Id, DialogMessages.FeedbackMessage, replyMarkup: keyboard);
        }
    }
}