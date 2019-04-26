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

        TaskDTO GetNextTaskInfo(Guid userId);

        string GetHint(Guid userId);

        bool? SendAnswer(Guid userId, string answer);
    }
}
