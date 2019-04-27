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
    internal class CheckTaskCommand : ICommand
    {
        private readonly string answer;

        public CheckTaskCommand(string answer)
        {
            this.answer = answer;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var state = user.CurrentState as TaskState;
            logger.LogInformation("Command: CheckTask");
            logger.LogInformation($"User: ID: {user.Id} TG:{user.TelegramId}");
            var isCorrect = quizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                logger.LogInformation($"The answer is {answer} and {isCorrect}");
                if (isCorrect.Value)
                {
                    await client.SendTextMessageAsync(chat.Id, "А ты прав!");
                    var progress = quizService.GetCurrentProgress(user.Id, 
                        Guid.Parse(state.TopicId), Guid.Parse(state.LevelId));
                    await client.SendTextMessageAsync(chat.Id, $"Прогресс:\n{progress}");
                }
                else await client.SendTextMessageAsync(chat.Id, "Подумай еще(");
            }
            else
            {
                await client.SendTextMessageAsync(chat.Id, "Что-то пошло не так(");
            }
        }
    }
}