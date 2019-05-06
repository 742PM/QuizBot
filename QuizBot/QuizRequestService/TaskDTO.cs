using System.Collections.Generic;

namespace QuizRequestService
{
    public class TaskDTO
    {
        public string Question;
        public IEnumerable<string> Answers;
        public bool HasHints;

        public TaskDTO(string question, IEnumerable<string> answers, bool hasHints)
        {
            Question = question;
            Answers = answers;
            HasHints = hasHints;
        }
    }
}