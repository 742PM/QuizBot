using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            logger.LogInformation("Command: CheckTask");
            logger.LogInformation($"User: ID: {user.Id}\n TG:{user.TelegramId}");
            var isCorrect = quizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                logger.LogInformation($"The answer is {answer} and {isCorrect}");
                if (isCorrect.Value)
                    await client.SendTextMessageAsync(chat.Id, "<p><span style=\"color: #88CC00\">Похоже на правду</span></p>", ParseMode.Html);
                await client.SendTextMessageAsync(chat.Id, "<p><span style=\"color:red\">Подумай еще</span></p>", ParseMode.Html);
            }
            else
            {
                await client.SendTextMessageAsync(chat.Id, "Что-то пошло не так(");
            }
        }
    }
}