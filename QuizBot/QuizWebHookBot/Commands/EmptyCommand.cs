using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Commands
{
    public class EmptyCommand : ICommand
    {
        public Task Execute(Message message, TelegramBotClient client)
        {
            return new Task(() => { });
        }
    }
}