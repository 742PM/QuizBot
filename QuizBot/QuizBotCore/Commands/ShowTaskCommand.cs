using System;
using System.Linq;
using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class ShowTaskCommand : ICommand
    {
        private readonly string levelId;

        public ShowTaskCommand(string levelId)
        {
            this.levelId = levelId;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            var chatId = chat.Id;
            var userId = Guid.Empty;
            var topicId = Guid.Empty;
            var levelGuid = Guid.Parse(levelId);

            var task = quizService.GetTaskInfo(userId, topicId, levelGuid);
            var messageText = task.Question;
            
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                task.Answers.Select(InlineKeyboardButton.WithCallbackData)
            });
            
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: keyboard);
        }
    }
}