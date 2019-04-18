using System.Threading.Tasks;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public class EmptyCommand : ICommand
    {
        public Task Execute(Message message, TelegramBotClient client)
        {
            return new Task(() => { });
        }

        /// <inheritdoc />
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();

        /// <inheritdoc />
        public Task ExecuteAsync(Message message, TelegramBotClient client, IQuizService quizService) => throw new System.NotImplementedException();
    }
}
