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
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new [] // first row
                {
                    InlineKeyboardButton.WithCallbackData("1.1"),
                    InlineKeyboardButton.WithCallbackData("1.2"),
                },
                new [] // second row
                {
                    InlineKeyboardButton.WithCallbackData("2.1"),
                    InlineKeyboardButton.WithCallbackData("2.2"),
                }
            });
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: inlineKeyboard);
//                replyMarkup: new InlineKeyboardMarkup(quizService.GetTopics().Select(x => x.Name)
//                    .Select(x => new InlineKeyboardButton{Text = x, CallbackData = x})));
        }

        /// <inheritdoc />
        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}