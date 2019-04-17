using FluentAssertions;
using NUnit.Framework;
using QuizWebHookBot;
using QuizWebHookBot.StateMachine;

namespace Tests
{
    [TestFixture]
    public class StateMachine_Should
    {
        public class TestState : State
        {
        }

        public class TestTransition : Transition
        {
        }

        public class Command
        {
            private readonly int exe;

            public Command(int exe)
            {
                this.exe = exe;
            }
            public int Execute() => exe;
        }

        [Test]
        public void AcceptItself()
        {   // State, Transition, TCommand
            var fsm = StateMachine.For<ICommand>()
                                  .Case<TestState, TestTransition>((TestState state,TestTransition transition)=>(default(State),default(ICommand)))
                                  .Build();
        }

        [Test]
        public void DoNothing_WhenEmpty()
        {
            StateMachine.For<Command>()
                                  .Build()
                                  .GetNextState(default(State), default(Transition))
                                  .Should()
                                  .BeEquivalentTo((State.Empty, default(Command)));
        }
        [Test]
        public void DoNothing_WhenStateOrTransitionIsNull()
        {
            StateMachine.For<Command>()
                        .Case<TestState, TestTransition>((state, _) => (state, new Command(1)))
                        .Build()
                        .GetNextState(default(State), default(Transition))
                        .Should()
                        .BeEquivalentTo((State.Empty, default(Command)));
        }

        [Test]
        public void ShouldProcessStates()
        {
            StateMachine.For<Command>()
                        .Case<TestState, TestTransition>((state,transition) => (state, new Command(1)))
                        .Build().GetNextState(new TestState(), new TestTransition()).command.Execute().Should().Be(1);
        }
    }
}