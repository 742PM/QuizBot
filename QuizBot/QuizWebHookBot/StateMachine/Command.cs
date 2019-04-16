using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot.StateMachine
{
    public delegate Task Command(Message message, TelegramBotClient client);
}
