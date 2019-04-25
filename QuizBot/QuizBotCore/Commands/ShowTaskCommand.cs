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
        private readonly string topicId;
        private readonly string levelId;

        public ShowTaskCommand(string topicId, string levelId)
        {
            this.topicId = topicId;
            this.levelId = levelId;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            var chatId = chat.Id;
            var userId = Guid.Empty;
            var topicGuid = Guid.Parse(topicId);
            var levelGuid = Guid.Parse(levelId);

//            var task = quizService.GetTaskInfo(userId, topicGuid, levelGuid);
//            var messageText = task.Question;

            var messageText = "Question";
            var answers = new[] {"O(1)", "O(n)"};
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                answers.Select(InlineKeyboardButton.WithCallbackData)
            });
            
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: keyboard);
        }
    }
}