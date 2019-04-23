using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(AboutState), typeof(AdminState), 
        typeof(UnknownUserState), typeof(WelcomeState))]
    public abstract class State
    {

        public abstract Transition[] AvailableTransitions { get; }
    }
}
