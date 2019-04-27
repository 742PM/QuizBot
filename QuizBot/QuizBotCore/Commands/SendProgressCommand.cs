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
        
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService, IUserRepository userRepository,
            ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var state = user.CurrentState as TaskState;
            var percentage = quizService.GetCurrentProgress(user.Id, 
                Guid.Parse(state.TopicId), Guid.Parse(state.LevelId));

            var progress = GenerateProgressBar(double.Parse(percentage), 1, 10);
            await client.SendTextMessageAsync(chat.Id, $"Прогресс:\n{progress}");
        }

        private string GenerateProgressBar(double percentage, int minSize, int maxSize)
        {
            const char filled = '⬤';
            const char empty = '◯';
            var totalFilled = (int)Math.Max(minSize, percentage * maxSize);
            return new string(filled, totalFilled).PadRight(maxSize, empty);
        }
    }
}