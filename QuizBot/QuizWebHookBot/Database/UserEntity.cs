using System;
using QuizWebHookBot.StateMachine;

namespace QuizWebHookBot.Database
{
    public class UserEntity
    {
        public UserEntity(Guid serviceId, State currentState, string telegramToken)
        {
            ServiceId = serviceId;
            CurrentState = currentState;
            TelegramToken = telegramToken;
        }

        public Guid ServiceId { get; set; }

        public State CurrentState { get; set; }

        public string TelegramToken { get; set; }
    }
}
