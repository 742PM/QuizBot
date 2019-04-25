using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using QuizBotCore.Database;
using QuizBotCore.States;
using static MongoDB.Bson.Serialization.BsonClassMap;

namespace QuizBotCore
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var db = CreateDatabase("mongodb://localhost:27017");
            var collection = db.GetCollection<UserEntity>("users");
            var user = new UserEntity(new LevelSelectionState("some id"), 3, Guid.NewGuid());
            Console.WriteLine(user.Id);
            collection.InsertOne(user);
            Console.WriteLine(user.Id);

        }
        public static IMongoDatabase CreateDatabase(string connectionString) =>
            new MongoClient(connectionString).GetDatabase("bot_test");
    }
}
