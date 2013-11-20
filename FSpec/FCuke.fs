module FSpec.FCuke

type Assertion =
  | Success
  | ExpectationFailure of obj * obj * string

let ( =?= ) expected actual = 
  if expected = actual then Success else
  ExpectationFailure (expected, actual, "Expectation not met")

type Step<'TState> = 
  | Given of ('TState -> Async<'TState>)
  | When of ('TState -> Async<'TState>)
  | Then of ('TState -> Async<unit>)

type Action<'TState> =
  | Background of Step<'TState> list
  | Scenario of string * Step<'TState> list

type Feature<'TState> = {
  Name : string
  Description : string
  InitialState : 'TState
  Actions : Action<'TState> list
  }

let Feature (description:string) (state:'TState) actionlist =
  let split = description.Split([|'\n'|], 2)
  { Name = split.[0]
    Description = split.[1]
    InitialState = state
    Actions = actionlist
    }

let Scenario name lst = Scenario (name, lst)

