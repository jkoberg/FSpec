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
 
let ``the client has authenticated with _`` authtype state = async {
  return {state with storage = Storage.JsonFileStorage.Default}
  }

let ``the client has negotiated for scope _`` scope state = async {
  return {state with httpClient = System.Net.Http.HttpClient.WithOAuth2("rest-1.v1 attachment.img", state.storage)}
  }

let ``a Member _ with Default Role _`` (membername, role) state = async {
  return {state with remoteMemberId = "" }
  }

let ``I create a new Attachment asset with the attributes`` attrs state = async {
  return {state with createdAttachmentId = ""}
  }

let ``It creates`` (state:State) = async {
  let! result = Async.AwaitTask <| state.httpClient.GetStringAsync("")
  assert (result.Length > 0)
  }

let ``the client has authenticated with <authtype>`` state (authtype:string, endpoint:string) = async {
  return {state with httpClient=state.httpClient}
  }
      