using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public class MessageParser : IMessageParser
    {
        /// <inheritdoc />
        public Transition Parse(State currentState, Message message) => throw new System.NotImplementedException();
    }
}