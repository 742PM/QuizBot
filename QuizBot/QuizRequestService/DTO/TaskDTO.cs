using System.Collections.Generic;

namespace QuizRequestService.DTO
{
    public class TaskDTO
    {
        public string Question { get; set; }
        public IEnumerable<string> Answers { get; set; }
        public bool HasHints { get; set; }

        public TaskDTO(string question, IEnumerable<string> answers, bool hasHints)
        {
            Question = question;
            Answers = answers;
            HasHints = hasHints;
        }
    }
}