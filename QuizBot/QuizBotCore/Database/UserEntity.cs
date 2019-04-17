using System;
using QuizBotCore.States;

namespace QuizBotCore.Database
{
    public class UserEntity
    {
        public UserEntity(Guid serviceId, State currentState, int telegramId)
        {
            ServiceId = serviceId;
            CurrentState = currentState;
            TelegramId = telegramId;
        }

        public Guid ServiceId { get; set; }

        public State CurrentState { get; set; }

        public int TelegramId { get; set; }
    }
}
