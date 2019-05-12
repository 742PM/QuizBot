using System.Threading.Tasks;
using QuizBotCore.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class ReportTaskCommand : ICommand
    {
        public ReportTaskCommand()
        {
        }
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            var messageReport = await client.SendTextMessageAsync(chat.Id, "Опишите вашу проблему:", 
                replyMarkup: new ForceReplyMarkup(), replyToMessageId: user.MessageId);
        }
    }
    
    internal class SendReportTaskCommand : ICommand
    {
        private readonly int id;

        public SendReportTaskCommand(int id)
        {
            this.id = id;
        }
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            await client.ForwardMessageAsync("@quibblereport", chat.Id, id);
            await client.ForwardMessageAsync("@quibblereport", chat.Id, user.MessageId);
            await client.SendTextMessageAsync(chat.Id, DialogMessages.Thanks);
            await new SelectTopicCommand().ExecuteAsync(chat, client, serviceManager);
        }
    }   
}