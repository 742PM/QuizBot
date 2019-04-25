using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(AboutState), typeof(AdminState), 
        typeof(UnknownUserState), typeof(WelcomeState), typeof(LevelSelectionState))]
    public abstract class State
    {
        [BsonElement]
        public abstract Transition[] AvailableTransitions { get; }
        
    }
}
