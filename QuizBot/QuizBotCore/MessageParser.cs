using System;
using QuizBotCore.States;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    public class MessageParser : IMessageParser
    {
        /// <inheritdoc />
        public Transition Parse(State currentState, Message message)
        {
            switch (currentState)
            {
                case UnknownUserState unknownUserState:
                    return UnknownUserStateParser(message);
                case WelcomeState welcomeState:
                    return WelcomeStateParser(message);
            }

            throw new NotImplementedException();
        }

        private Transition WelcomeStateParser(Message message)
        {
            throw new NotImplementedException();
        }

        private Transition UnknownUserStateParser(Message message)
        {
            return new BackTransition();
        }
    }
}
