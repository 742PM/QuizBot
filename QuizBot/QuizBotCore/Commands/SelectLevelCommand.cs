using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    public class SelectLevelCommand : ICommand
    {
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            throw new System.NotImplementedException();
        }
    }
}