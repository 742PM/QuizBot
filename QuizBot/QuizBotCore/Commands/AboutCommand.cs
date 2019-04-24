using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    internal class AboutCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            await client.SendTextMessageAsync(chat.Id, "Тут будет некая справочка");
        }
    }
}