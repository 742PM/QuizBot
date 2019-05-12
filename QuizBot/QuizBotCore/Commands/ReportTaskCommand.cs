using System.Threading.Tasks;
using QuizBotCore.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class ReportTaskCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            await client.SendTextMessageAsync(chat.Id, DialogMessages.ReportRequesting,
                replyMarkup: new ForceReplyMarkup(), replyToMessageId: user.MessageId);
        }
    }

    internal class SendReportTaskCommand : ICommand
    {
        private readonly int messageId;
        private const string ReportContact = "@quibblereport";

        public SendReportTaskCommand(int messageId)
        {
            this.messageId = messageId;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            var reportUserInfo = $"Report message from: {chat.Id};" +
                                 $"UserId: {user.Id}, State: {user.CurrentState}";
            await client.SendTextMessageAsync(ReportContact, reportUserInfo);
            await client.ForwardMessageAsync(ReportContact, chat.Id, messageId);
            await client.ForwardMessageAsync(ReportContact, chat.Id, user.MessageId);
            await client.SendTextMessageAsync(chat.Id, DialogMessages.ReportThanks);
            await new SelectTopicCommand().ExecuteAsync(chat, client, serviceManager);
        }
    }
}