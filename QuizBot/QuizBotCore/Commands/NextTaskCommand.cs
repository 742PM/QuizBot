using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class NextTaskCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService, IUserRepository userRepository,
            ILogger logger)
        {
            var user = userRepository.FindByTelegramId(chat.Id);
            var task = quizService.GetNextTaskInfo(user.Id);

            var messageText = task.Question;

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task.Answers.Select(InlineKeyboardButton.WithCallbackData),
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Next", "next")
                }
            });
            
            await client.SendTextMessageAsync(chat.Id, messageText, replyMarkup: keyboard);
        }
    }
}