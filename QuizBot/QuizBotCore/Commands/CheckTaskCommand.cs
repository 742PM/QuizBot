using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore
{
    internal class CheckTaskCommand : ICommand
    {
        private readonly string answer;

        public CheckTaskCommand(string answer)
        {
            this.answer = answer;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService, IUserRepository userRepository)
        {
            var user = userRepository.FindByTelegramId(chat.Id);

            var isCorrect = quizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                if (isCorrect.Value)
                    await client.SendTextMessageAsync(chat.Id, "<p><span style=\"color: #88CC00\">Похоже на правду</span></p>", ParseMode.Html);
                await client.SendTextMessageAsync(chat.Id, "<p><span style=\"color:red\">Подумай еще</span></p>", ParseMode.Html);
            }
        }
    }
}