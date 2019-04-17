namespace StateMachine.States
open System

type State = 
    | God //Unknown or Zero
    | Admin  //At admin screen
    | About
    | LevelSelection of Guid
    | TopicSelection of Guid
    | Task
    | Welcome

