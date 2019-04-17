using System.Threading.Tasks;
using QuizWebHookBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot
{
    public interface ICommand
    {
        Task ExecuteAsync(Chat message, IBotService client);
    }
}