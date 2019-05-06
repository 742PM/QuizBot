namespace QuizRequestService
{
    public class ProgressDTO
    {
        public int TasksCount;
        public int TasksSolved;

        public ProgressDTO(int tasksCount, int tasksSolved)
        {
            TasksCount = tasksCount;
            TasksSolved = tasksSolved;
        }
    }
}