using System;
using RestSharp;

namespace QuizRequestService
{
    public interface IQuizService
    {
        string GetTopics();

        string GetLevels(Guid topicId);

        string GetAvailableLevels(Guid userId, Guid topicId);

        string GetCurrentProgress(Guid userId, Guid topicId, Guid levelId);

        string GetTaskInfo(Guid userId, Guid topicId, Guid levelId);

        string GetNextTaskInfo(Guid userId);

        string GetHint(Guid userId);

        string SendAnswer(Guid userId, string answer);
    }
}
