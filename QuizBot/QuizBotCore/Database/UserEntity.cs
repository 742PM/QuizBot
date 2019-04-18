using System;
using System.Diagnostics.CodeAnalysis;
using QuizBotCore.States;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace QuizBotCore.Database
{
    public struct UserEntity
    {
        public UserEntity(Guid serviceId, State currentState, int telegramId)
        {
            ServiceId = serviceId;
            CurrentState = currentState;
            TelegramId = telegramId;
        }

        public Guid ServiceId { get; private set; }

        public State CurrentState { get; private set; }

        public int TelegramId { get; private set; }
    }
}
