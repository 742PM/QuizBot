using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot
{
    public interface ICommand
    {
        string Command { get; }
        Task Execute(Message message, TelegramBotClient client);
        bool Contains(Message message);
    }
}