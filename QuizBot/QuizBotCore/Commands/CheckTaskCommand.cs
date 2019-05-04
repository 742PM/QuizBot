using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
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
            var isCorrect = quizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                if (isCorrect.Value)
                {
                    await client.SendTextMessageAsync(chat.Id, DialogMessages.CheckTaskCorrect);
                    await new SendProgressCommand().ExecuteAsync(chat, client,quizService,userRepository,logger);
                    await new NextTaskCommand().ExecuteAsync(chat, client, quizService, userRepository, logger);
                }
                else await client.SendTextMessageAsync(chat.Id, DialogMessages.CheckTaskWrong);
            }
        }
    }
}