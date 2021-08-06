let answer =
    [0..1000]
    |> List.filter (fun x -> x % 3 = 0 || x % 5 = 0)
    |> List.sum

printfn "%i" answer
