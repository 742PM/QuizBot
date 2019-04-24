using System.Linq;
using System.Threading.Tasks;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class WelcomeCommand : ICommand
    {
        /// <inheritdoc />
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            var chatId = chat.Id;
            var userName = chat.Username;
            var messageText = $"Привет. Я QuibbleBot, а ты {userName}. " +
                              "Приятно познакомиться) " +
                              "В наличие вот такое добро, хочешь порешать?";
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                quizService.GetTopics().Select(x => x.Name).Select(x => InlineKeyboardButton.WithCallbackData(x,x))
            });
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: keyboard);
        }

        /// <inheritdoc />
        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}