module FSpec.Run

open FCuke

let runStep step state =
  match step with
  | Given f -> f state
  | When f -> f state
  | Then t -> async {
    do! t state
    return state
    }

let rec runSteps state steps = async {
    match steps with
    | [] -> return state
    | step::steps ->
      let! newstate = runStep step state
      return! runSteps newstate steps
    }

let rec runActions prevState actions = async {
  match actions with
    | [] -> return prevState
    | action::moreActions -> 
      match action with
      | Background steps ->
        let! stateWithBackground = runSteps prevState steps
        return! runActions stateWithBackground moreActions
      | Scenario (name, steps) ->
        let! endOfScenarioState = runSteps prevState steps
        return! runActions prevState moreActions
  }

let runFeature (f:Feature<_>) = async {
  let! final = runActions f.InitialState f.Actions
  ignore final
  }
    
