using System.Threading.Tasks;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public class EmptyCommand : ICommand
    {
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository) => new Task(() => { });

    }
}
