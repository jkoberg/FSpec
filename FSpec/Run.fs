module FSpec.Run

open FCuke

type RunState<'TArgs> = { args: 'TArgs }
  with static member Default = {args=[]}

let runStep step theirstate =
  match step with
  | Given f -> f theirstate
  | When f -> f theirstate
  | Then t -> async {
    do! t theirstate
    return theirstate
    }

let rec runSteps theirstate steps = async {
    match steps with
    | [] -> return theirstate
    | step::steps ->
      let! theirstate = runStep step theirstate
      return! runSteps theirstate steps
    }

let rec runActions prevstate actions = async {
  match actions with
    | [] -> return prevstate
    | action::moreActions -> 
      match action with

      | Background steps ->
        let! withBackgroundState = runSteps prevstate steps
        return! runActions withBackgroundState moreActions

      | Scenario (name, steps) ->
        let! afterScenarioState = runSteps prevstate steps
        return! runActions prevstate moreActions

      | ScenarioOutline (name, stepsOf, examples) ->
        for ex in examples do
          let! endstate = runSteps prevstate <| stepsOf ex
          ignore endstate
        return! runActions prevstate moreActions      
  }

let runFeature (f:Feature<_,_>) = async {
  let! final = runActions f.InitialState f.Actions
  ignore final
  }
    
