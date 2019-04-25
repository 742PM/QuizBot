using System;
using MongoDB.Bson.Serialization.Attributes;
using QuizBotCore.States;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace QuizBotCore.Database
{
    public class UserEntity 
    {
        [BsonConstructor]
        public UserEntity(State currentState, long telegramId, Guid id)
        {
            CurrentState = currentState;
            TelegramId = telegramId;
            Id = id;
        }
        [BsonId]
        public Guid Id { get; private set; }

        [BsonElement]
        public State CurrentState { get; private set; }

        [BsonElement]
        public long TelegramId { get; private set; }
    }
    

}
