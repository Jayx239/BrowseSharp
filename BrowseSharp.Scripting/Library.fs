namespace BrowseSharp.Scripting

module Say =
    let hello name =
        printfn "Hello %s" name
    let somethingToDo () =
        0
        
    let doSomethingIn (s) =
        somethingToDo s 
    let doSomethingOut () =
        somethingToDo()