using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(AboutState), typeof(AdminState), typeof(UnknownUserState))]
    public abstract class State
    {

        public abstract Transition[] AvailableTransitions { get; }
    }
}
