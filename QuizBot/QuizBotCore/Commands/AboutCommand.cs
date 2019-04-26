using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore
{
    internal class AboutCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var message = " ***Quibble*** происходит от двух фундаментальных слов:\n" +
                          "*Quiz* + *Bible* - задаем вопросы, обучаем и даем ответы...\n" +
                          "Обучение новому материалу проходит в виде бесконечной викторины.\n" +
                          "Пройти уровень можно только решив определенное количество задач подряд.\n" +
                          "*Желаем удачи!* ";
            await client.SendTextMessageAsync(chat.Id, message, ParseMode.Markdown);
        }
    }
}