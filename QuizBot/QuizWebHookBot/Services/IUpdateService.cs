using System.Threading.Tasks;
using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        State GetUserState(Message message);
        ICommand RecognizeCommand(Message message);
        Task ExecuteCommand(ICommand command, Message message);
    }
}
