using System;

namespace QuizBotCore
{
    internal class CircleProgressBar : IProgressBar
    {
        public string GenerateProgressBar(double percentage, int minSize, int maxSize)
        {
            var totalFilled = (int) Math.Max(minSize, percentage * maxSize);
            return new string(DialogMessages.ProgressFilled, totalFilled)
                .PadRight(maxSize, DialogMessages.ProgressEmpty);
        }
    }
}