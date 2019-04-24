using System;
using System.Collections.Generic;

namespace QuizRequestService
{
    public interface IQuizService
    {
        IEnumerable<TopicDTO> GetTopics();

        IEnumerable<LevelDTO> GetLevels(Guid topicId);

        string GetAvailableLevels(Guid userId, Guid topicId);

        string GetCurrentProgress(Guid userId, Guid topicId, Guid levelId);

        TaskDTO GetTaskInfo(Guid userId, Guid topicId, Guid levelId);

        string GetNextTaskInfo(Guid userId);

        string GetHint(Guid userId);

        string SendAnswer(Guid userId, string answer);
    }
}
