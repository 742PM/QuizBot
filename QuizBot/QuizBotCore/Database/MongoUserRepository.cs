using System;
using MongoDB.Driver;

namespace QuizBotCore.Database
{
    internal class MongoUserRepository : IUserRepository
    {
        public const string CollectionName = "users";
        private readonly IMongoCollection<UserEntity> userCollection;

        public MongoUserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<UserEntity>(CollectionName);
        }

        /// <inheritdoc />
        public UserEntity Insert(UserEntity user) => throw new NotImplementedException();

        /// <inheritdoc />
        public UserEntity FindById(int id) => throw new NotImplementedException();

        /// <inheritdoc />
        public void Update(UserEntity user)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public UserEntity UpdateOrInsert(UserEntity user) => throw new NotImplementedException();

        /// <inheritdoc />
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
