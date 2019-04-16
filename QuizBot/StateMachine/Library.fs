namespace StateMachine
open System
open System.Threading.Tasks

type UserToken = UserToken of string //я не уверен, что это так, поэтому обертка

type State = 
    | Welcome of UserToken
    | Welcomed
    | TopicSelection of Guid  //LevelId тип
    | LevelSelection of Guid   // TopicId
    | SolvingTask of (Task * Guid) //пример

module StateMachine =

    let GetNextState (state: State): State =
        match state with
        | Welcome token -> Welcomed
        | TopicSelection id -> LevelSelection (id)
        | LevelSelection id -> SolvingTask (new Task(null), id)


