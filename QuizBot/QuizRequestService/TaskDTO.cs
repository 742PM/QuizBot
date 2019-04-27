using System.Collections.Generic;

namespace QuizRequestService
{
    public class TaskDTO
    {
        public string Question;
        public IEnumerable<string> Answers;

        public TaskDTO(string question, IEnumerable<string> answers)
        {
            Question = question;
            Answers = answers;
        }
    }
}