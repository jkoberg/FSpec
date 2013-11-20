module SpecTests

open FSpec.FCuke

open StepDefs

let features = [

  Feature """OAuth2 Bearer

    As an automated client,
    I expect to see headers that comply with Bearer header standards,
    so I know when to use OAuth2 and what scopes to request.""" State.Default [

    Background [
      Given ``the client has authenticated with OAuth 2.0``
      Given ``the client has negotiated for "rest-1.v1" and "attachment.img" scopes``
      Given ``and a Member "remote" with Default Role "Customer" (or above)``
      ]

    Scenario "Create a new attachment asset" [
      When ``I create a new Attachment asset with the following attributes``
      Then ``It creates``
      ]

    ]

  ]

