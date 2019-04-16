using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Controllers
{
    public delegate Task ProcessUpdate(Message message);
}
