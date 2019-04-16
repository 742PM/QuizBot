using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizWebHookBot.StateMachine
{
    class WelcomeState : State
    {
        private ICommand command;
        private Message message;
        private TelegramBotClient client;
        /// <inheritdoc />
        public WelcomeState(Guid userId, ICommand command, Message message, TelegramBotClient client) : base(userId)
        {
            this.command = command;
            this.message = message;
            this.client = client;
        }

        public State ProcessState()
        {
            this.command.Execute(message, client);
            return this;
        }
        //TODO: there goes something
    }
}