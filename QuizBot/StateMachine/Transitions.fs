namespace StateMachine.Transitions
open System



type Transition = 
        | About
        | Admin of Guid
        | Back
        | Select of Guid
        | Next
        | Skip
        | Answer of string
module Transition =
    let About() = About



