using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using QuizBotCore.States;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace QuizBotCore.Database
{
    public struct UserEntity 
    {
        [BsonConstructor]
        public UserEntity(Guid serviceId, State currentState, int telegramId, Guid id)
        {
            ServiceId = serviceId;
            CurrentState = currentState;
            TelegramId = telegramId;
            Id = id;
        }
        [BsonId]
        public Guid Id { get; }

        [BsonElement]
        public Guid ServiceId { get;  }

        public State CurrentState { get;  }

        public int TelegramId { get;  }
    }
    

}
