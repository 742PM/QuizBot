using System.Threading.Tasks;
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
        /// <inheritdoc />
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();

        /// <inheritdoc />
//        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}