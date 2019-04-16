using System.Linq;
using System.Threading.Tasks;
using QuizWebHookBot.QuizBackendRequests;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizWebHookBot.Commands
{
    public class Welcome
    {
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var api = new QuizApi();
            var userName = message.From.Username;
            var topics = await api.GetTopics();
            var messageText = $"Привет. Я QuibbleBot, а ты {userName}. " +
                              "Приятно познакомиться)" +
                              "В наличие вот такое добро, хочешь порешать?";
            await client.SendTextMessageAsync(chatId, messageText,
                                              replyMarkup: new InlineKeyboardMarkup(topics.Select(x => x.Name)
                                                                                          .Select(x =>
                                                                                                      new
                                                                                                          InlineKeyboardButton
                                                                                                          {
                                                                                                              Text = x
                                                                                                          })));
        }
    }
}
