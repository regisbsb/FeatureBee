open canopy
open canopy.configuration
open runner
open System

// let canopy.configuration.phantomJSDir = ".\"
canopy.configuration.phantomJSDir <- @".\"

//start an instance of the browser
start phantomJS


//this is how you define a test
"Startup FeatureBee and check initialization of database" &&& fun _ ->
    //this is an F# function body, it's whitespace enforced

    //go to url
    url "http://localhost:51100/"

    displayed "#featureBeeLogo"

//run all tests
run()

quit()