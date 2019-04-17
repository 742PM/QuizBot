using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IMessageParser
    {
        Transition Parse(State currentState, Message message);
    }
}