using System;
using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    internal class FeedBackCommand : ICommand
    {
        public Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            throw new NotImplementedException();
        }
    }
}