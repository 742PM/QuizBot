using System.Threading.Tasks;
using QuizBotCore.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

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
//            var messageReport = await client.SendTextMessageAsync(chat.Id, "Опишите вашу проблему:", 
//                replyMarkup: new ForceReplyMarkup(), replyToMessageId: user.MessageId);
            await client.ForwardMessageAsync("@quibblereport", chat.Id, user.MessageId);
        }
    }    
}