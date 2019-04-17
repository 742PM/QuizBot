using System;

namespace QuizBotCore.Database
{
    public interface IUserRepository
    {
        UserEntity Insert(UserEntity user);
        UserEntity FindById(int id);

        void Update(UserEntity user);
        UserEntity UpdateOrInsert(UserEntity user);
        void Delete(Guid id);
    }
}
