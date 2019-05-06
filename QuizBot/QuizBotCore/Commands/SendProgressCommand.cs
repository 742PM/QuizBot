using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.States;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    internal class SendProgressCommand : ICommand
    {
        public async Task ExecuteAsync(
            Chat chat, 
            TelegramBotClient client, 
            IQuizService quizService,
            IUserRepository userRepository,
            ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var state = user.CurrentState as TaskState;
            var percentage = quizService.GetCurrentProgress(user.Id,
                state.TopicDTO.Id, state.LevelDTO.Id);
            if (percentage != null)
            {
                var progressInPercencts = double.Parse(percentage);
                var progress = GenerateProgressBar(progressInPercencts, 1, 10);
                await client.SendTextMessageAsync(chat.Id, DialogMessages.ProgressMessage + progress);
                if (progressInPercencts == 1.0)
                    await client.SendTextMessageAsync(chat.Id, DialogMessages.LevelCompleted);
            }

        }

        private string GenerateProgressBar(double percentage, int minSize, int maxSize)
        {
            var totalFilled = (int) Math.Max(minSize, percentage * maxSize);
            return new string(DialogMessages.ProgressFilled, totalFilled)
                .PadRight(maxSize, DialogMessages.ProgressEmpty);
        }
    }
}