using System.Threading.Tasks;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class WelcomeCommand : ICommand
    {
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var userName = message.From.Username;
            var messageText = $"Привет. Я QuibbleBot, а ты {userName}. " +
                              "Приятно познакомиться)" +
                              "В наличие вот такое добро, хочешь порешать?";
            //await client.SendTextMessageAsync(chatId, messageText,
              //                                replyMarkup: new InlineKeyboardMarkup(topics.Select(x => x.Name)
                //                                                                          .Select(x =>
                  //                                                                                    new
                    //                                                                                      InlineKeyboardButton
                      //                                                                                    {
                        //                                                                                      Text = x
                          //                                                                                })));
        }

        /// <inheritdoc />
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();

        /// <inheritdoc />
        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}