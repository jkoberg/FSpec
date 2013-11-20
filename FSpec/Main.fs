module Main

open FSpec.Run

let asyncMain argv = async {
  for feature in SpecTests.features do
    do! runFeature feature
  }

  

