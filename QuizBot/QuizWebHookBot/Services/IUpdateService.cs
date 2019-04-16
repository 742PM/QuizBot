using System.Threading.Tasks;
using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        Command GetUserState(Message message);
        Command RecognizeCommand(Message message);
        Task ExecuteCommand(Command command, Message message);
    }
}
