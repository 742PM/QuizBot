
using System.Threading.Tasks;
using QuizBotCore.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

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
            long reportAccount = 349845203;
            await client.ForwardMessageAsync(chat.Id, reportAccount, messageId);
        }
    }
}