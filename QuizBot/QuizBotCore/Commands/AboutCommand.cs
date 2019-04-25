using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    internal class AboutCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository)
        {
            await client.SendTextMessageAsync(chat.Id, "Тут будет некая справочка");
        }
    }
}