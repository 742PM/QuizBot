using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot
{
    public interface ICommand
    {
        Task Execute(Message message, TelegramBotClient client);
    }
}