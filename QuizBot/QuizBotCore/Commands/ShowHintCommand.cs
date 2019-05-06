using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    internal class ShowHintCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService, IUserRepository userRepository,
            ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var hint = quizService.GetHint(user.Id);
            if (hint == null)
                await client.SendTextMessageAsync(chat.Id, DialogMessages.NoHintsMessage);
            else await client.SendTextMessageAsync(chat.Id, hint.HintText);
        }
    }
}