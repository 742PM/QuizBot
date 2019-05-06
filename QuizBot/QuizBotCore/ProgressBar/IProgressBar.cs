namespace QuizBotCore
{
    interface IProgressBar
    {
        string GenerateProgressBar(int solved, int total);
    }
}