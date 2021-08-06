let rec fibBottomUp = function
    | (x, Some acc) when x >= 4000000 ->
        acc
    | (x, Some acc) ->
        let nextFib = ((x + (List.head acc)) :: acc)
        let x' = x + 1
        fibBottomUp (x', Some nextFib)
    | (_, None) ->
        fibBottomUp (2, Some [1; 1])

let fibBottomUpOpt =
    let mutable count: int64 = 2
    let mutable prev: int64 = 1
    let mutable x: int64 = 2
    while x < 4_000_000 do
        prev <- x + prev
        count <- count + prev
        x <- x + 1
    count


fibBottomUpOpt
|> printfn "%O"
