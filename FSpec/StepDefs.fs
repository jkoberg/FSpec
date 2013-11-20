module StepDefs

open OAuth2Client

type State =
  { secrets : Secrets option
    storage : IStorageAsync
    httpClient : System.Net.Http.HttpClient
    remoteMemberId: string
    createdAttachmentId: string
    }
  static member Default = {
    secrets = None
    storage = Storage.JsonFileStorage.Default
    httpClient = null
    remoteMemberId = null
    createdAttachmentId = null
    }
 
let ``the client has authenticated with OAuth 2.0`` state = async {
  return {state with storage = Storage.JsonFileStorage.Default}
  }

let ``the client has negotiated for "rest-1.v1" and "attachment.img" scopes`` state = async {
  return {state with httpClient = System.Net.Http.HttpClient.WithOAuth2("rest-1.v1 attachment.img", state.storage)}
  }

let ``and a Member "remote" with Default Role "Customer" (or above)`` state = async {
  return {state with remoteMemberId = "" }
  }

let ``I create a new Attachment asset with the following attributes`` state = async {
  return {state with createdAttachmentId = ""}
  }

let ``It creates`` (state:State) = async {
  let! result = Async.AwaitTask <| state.httpClient.GetStringAsync("")
  assert (result.Length > 0)
  }
      