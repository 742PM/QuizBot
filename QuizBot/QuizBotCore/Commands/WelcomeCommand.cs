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
                              "Выбирай, чем сегодня будем заниматься)";
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new [] // first row
                {
                    InlineKeyboardButton.WithCallbackData("Порешать задачки", "topics"),
                    InlineKeyboardButton.WithCallbackData("Справка", "info"),
                    InlineKeyboardButton.WithCallbackData("Обратная связь", "feedback")
                },
            });
//            var keyboard = new InlineKeyboardMarkup(new[]
//            {
//                quizService.GetTopics().Select(x => x.Name).Select(x => InlineKeyboardButton.WithCallbackData(x))
//            });
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: inlineKeyboard);
        }

        /// <inheritdoc />
        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}