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
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;
        private readonly string answer;

        public CheckTaskCommand(TopicDTO topicDto, LevelDTO levelDto, string answer)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
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
                    await new ShowTaskCommand(topicDto,levelDto, true).ExecuteAsync(chat, client, quizService, userRepository, logger);
                }
                else await client.SendTextMessageAsync(chat.Id, DialogMessages.CheckTaskWrong);
            }
        }
    }
}