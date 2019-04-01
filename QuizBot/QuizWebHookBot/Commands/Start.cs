using System;
using System.Linq;
using System.Threading.Tasks;
using QuizWebHookBot.QuizBackendRequests;
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
            var api = new QuizApi();
            var userName = message.From.Username;
            var topics = await api.GetTopics();
            await client.SendTextMessageAsync(chatId,
                $"Привет. Я QuibbleBot, а ты {userName}. " +
                "Приятно познакомиться)" +
                "У меня тут есть тема, хочешь порешать?",
                replyMarkup: new InlineKeyboardMarkup(topics.Select(x => x.Name)
                    .Select(x => new InlineKeyboardButton{Text = x})
                 ));
        }

        public bool Contains(Message message)
        {
            return message.Type == MessageType.Text && message.Text.Contains(Command);
        }
    }
}