using System.Collections.Generic;

namespace QuizRequestService.DTO
{
    public class TaskDTO
    {
        public string Question { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Answers { get; set; }
        public bool HasHints { get; set; }

        public TaskDTO(string question, string text, IEnumerable<string> answers, bool hasHints)
        {
            Question = question;
            Text = text;
            Answers = answers;
            HasHints = hasHints;
        }
    }
}