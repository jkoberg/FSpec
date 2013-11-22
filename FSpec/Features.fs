module SpecTests

open FSpec.FCuke

open StepDefs


let features = [

  Feature """OAuth2 Bearer

    As an automated client,
    I expect to see headers that comply with Bearer header standards,
    so I know when to use OAuth2 and what scopes to request.""" State.Default [

    Background [
      Given <| ``the client has authenticated with _`` "Oauth2"
      Given <| ``the client has negotiated for scope _`` "rest-1.v1"
      Given <| ``a Member _ with Default Role _`` ("remote", "Customer")
      ]

    Scenario "Create a new attachment asset" [
      When <| ``I create a new Attachment asset with the attributes`` [
        "ID", "Story:1001"
        "ContentType", "image/jpeg"
        ]
      Then ``It creates``
      ]

    ScenarioOutline "Different Auth types" (fun ex ->
      [ Given <| ``the client has authenticated with _`` ex ])
      [ "oauth2"
        "basic"
        ]
    ]
    
  ]
