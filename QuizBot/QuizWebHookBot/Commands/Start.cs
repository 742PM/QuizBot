using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizWebHookBot.Commands
{
    public class Start : ICommand
    {
        public string Command => @"/start";

        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var userName = message.From.Username;
            await client.SendTextMessageAsync(chatId, 
                 $"Привет. Я QuibbleBot, а ты {userName}. " +
                        "Приятно познакомиться)" +
                        "У меня тут есть тема, хочешь порешать?", 
                 replyMarkup: new InlineKeyboardMarkup(
                     new InlineKeyboardButton[]{"Complexity", "History"}
                 ));
        }

        public bool Contains(Message message)
        {
            return message.Type == MessageType.Text && message.Text.Contains(Command);
        }
    }
}