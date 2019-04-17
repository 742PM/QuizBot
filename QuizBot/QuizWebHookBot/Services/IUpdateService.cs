using System.Threading.Tasks;
using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        ICommand ProcessMessage(Message message);
    }
}
