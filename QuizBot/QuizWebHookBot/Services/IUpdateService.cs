using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
