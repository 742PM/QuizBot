using System.Threading.Tasks;
using QuizRequestService;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(Chat chat, IBotService client, IQuizService quizService);
        Task ExecuteAsync(Message message, IBotService client, IQuizService quizService);
    }
}
