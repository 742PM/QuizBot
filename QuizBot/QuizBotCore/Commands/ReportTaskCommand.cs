using System.Threading.Tasks;
using QuizBotCore.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class ReportTaskCommand : ICommand
    {
        private readonly int messageId;

        public ReportTaskCommand(int messageId)
        {
            this.messageId = messageId;
        }
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            await client.SendTextMessageAsync("@quibblereport", "Опишите вашу проблему:", replyMarkup: new ForceReplyMarkup(), replyToMessageId: messageId);
//            await client.ForwardMessageAsync("@quibblereport", chat.Id, messageId);
        }
    }    
}