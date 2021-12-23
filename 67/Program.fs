// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let fileName = "test.txt"

let readLines (filePath:string): seq<string> = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
    yield sr.ReadLine ()
}

// slow to parse ~4s
let data =
    let parseLine (line: string) = line.Split ' ' |> Array.map Int32.Parse
    readLines fileName
    |> Seq.map parseLine
    |> Seq.toArray

// brute force
let solve pyramid =
    let rec solver dept pos =
        let score = Array.get (Array.get pyramid dept) pos
        if Array.length pyramid - 1 = dept then score
        else
            let path1 = solver (dept + 1) pos
            let path2 = solver (dept + 1) (pos + 1)
            score + (max path1 path2)
    solver 0 0

// greedy 6580 (bad)
let greedy pyramid =
    let rec loop score dept pos =
        if Array.length pyramid - 1 = dept then score
        else
            let path1 = Array.get (Array.get pyramid (dept + 1)) pos
            let path2 = Array.get (Array.get pyramid (dept + 1)) (pos + 1)
            if path1 > path2 then
                loop (score + path1) (dept + 1) pos
            else
                loop (score + path2) (dept + 1) (pos + 1)
    loop (Array.get (Array.get pyramid 0) 0) 0 0



[<EntryPoint>]
let main argv =
    greedy data |> printfn "%i"
    0 // return an integer exit code
