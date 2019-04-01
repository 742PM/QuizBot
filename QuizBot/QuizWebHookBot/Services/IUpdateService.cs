using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        ICommand RecognizeCommand(Message message);
        Task ExecuteCommand(ICommand command, Message message);
    }
}
