// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let fileName = "path.txt"

let readLines (filePath:string): seq<string> = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
    yield sr.ReadLine ()
}

let data =
    let parseLine (line: string) = line.Split ' ' |> Array.map Int32.Parse
    readLines fileName
    |> Seq.map parseLine
    |> Seq.toArray

let rec solve pyramid dept pos =
    let score = Array.get (Array.get pyramid dept) pos
    if Array.length pyramid - 1 = dept then score
    else score + (max (solve pyramid (dept + 1) pos) (solve pyramid (dept + 1) (pos + 1)))

[<EntryPoint>]
let main argv =
    solve data 0 0 |> printfn "%i"
    0 // return an integer exit code
