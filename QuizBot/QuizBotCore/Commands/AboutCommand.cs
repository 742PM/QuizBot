using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
            IUserRepository userRepository, ILogger logger)
        {
            var message = "Quibble происходит от двух фундаментальных слов:\n" +
                          "Quiz + Bible - Задаем вопросы, обучаем и даем ответы...\n" +
                          "Обучение новому материалу проходит в виде бесконечной викторины.\n" +
                          "Пройти уровень можно только решив все задачи подряд без ошибки\n" +
                          "Желаем удачи";
            await client.SendTextMessageAsync(chat.Id, message);
        }
    }
}