using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var chatId = chat.Id;
            var user = userRepository.FindByTelegramId(chat.Id);
            var topicGuid = Guid.Parse(topicId);
            var levelGuid = Guid.Parse(levelId);

            var task = quizService.GetTaskInfo(user.Id, topicGuid, levelGuid);
            var question = task.Question;
            
            var questionInMarkdown = "```csharp\n" +
                                     $"{question}\n" +
                                     "```";

            var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    task
                        .Answers
                        .Select(x => new KeyboardButton(x)),
                    new[]
                    {
                        new KeyboardButton(ButtonNames.Back),
                        new KeyboardButton(ButtonNames.Hint),
                        new KeyboardButton(ButtonNames.NextTask)
                    }
                },
                resizeKeyboard: true,
                oneTimeKeyboard: true
            );

            await client.SendTextMessageAsync(chatId, questionInMarkdown, replyMarkup: keyboard,
                parseMode: ParseMode.Markdown);
        }
    }
}