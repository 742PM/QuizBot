using System;
using QuizBotCore.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore
{
    public class MessageParser : IMessageParser
    {
        /// <inheritdoc />
        public Transition Parse(State currentState, Update update)
        {
            switch (currentState)
            {
                case UnknownUserState unknownUserState:
                    return UnknownUserStateParser(update);
                case WelcomeState welcomeState:
                    return WelcomeStateParser(update);
                case TopicSelectionState topicSelection:
                    return TopicSelectionStateParser(update);
            }

            throw new NotImplementedException();
        }

        private Transition TopicSelectionStateParser(Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                if (callbackData != null) 
                    return new CorrectTransition(callbackData);
            }
            return new InvalidTransition();
        }

        private Transition WelcomeStateParser(Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                switch (callbackData)
                {
                    case "topics":
                        return new CorrectTransition("topics");
                    case "info":
                        return new CorrectTransition("topics");
                    case "feedback":
                        return new CorrectTransition("feedback");
                }
            }
            return new InvalidTransition();
        }

        private Transition UnknownUserStateParser(Update update)
        {
            return new BackTransition();
        }
    }
}
