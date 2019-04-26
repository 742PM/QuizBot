using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    class InvalidActionCommand : ICommand
    {
        private readonly string message;

        public InvalidActionCommand(string message)
        {
            this.message = message;
        }
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger) => throw new System.NotImplementedException();

    }
}