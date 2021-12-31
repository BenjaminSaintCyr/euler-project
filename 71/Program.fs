open System
open euler


let calcNum denum = 3 * denum / 7

let maxFractions fractions = 
    let compareFraction op (maxNum, maxDenum) (num, denum) = op ((int64 num) * (int64 maxDenum)) ((int64 maxNum) * (int64 denum))
    let maxFraction a b = if compareFraction (<) a b then a else b
    fractions
    |> Seq.fold maxFraction (2, 5)

[<EntryPoint>]
let main argv =
    let n = 1_000_000
    let isValid i j = 5 * j > i * 2 && 3 * i > j * 7 && Euler.gcd i j = 1
    seq {
        for i = Euler.highestPrime n to n do
             let mutable j = calcNum i
             while j < i && not <| isValid i j do j <- j + 1
             if isValid i j then (j, i)
    }
    |> maxFractions
    ||> printfn "%i / %i"
    0
