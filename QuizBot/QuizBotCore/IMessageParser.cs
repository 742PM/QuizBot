using QuizBotCore.States;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    public interface IMessageParser
    {
        Transition Parse(State currentState, Message message);
    }
}
