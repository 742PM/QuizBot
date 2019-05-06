namespace QuizBotCore
{
    interface IProgressBar
    {
        string GenerateProgressBar(double percentage, int minSize, int maxSize);
    }
}