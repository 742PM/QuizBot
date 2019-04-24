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
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();

    }
}