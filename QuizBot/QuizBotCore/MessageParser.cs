using System;
using QuizBotCore.States;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    public class MessageParser : IMessageParser
    {
        /// <inheritdoc />
        public Transition Parse(State currentState, Message message) => throw new NotImplementedException();
    }
}
