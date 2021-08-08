open System

module Euler =
    let IsPrime n =
        let sqrt' = (float >> sqrt >> int) n // square root of integer
        [ 2 .. sqrt' ] // all numbers from 2 to sqrt'
        |> List.forall (fun x -> n % x <> 0) // no divisors

    let AllPrime n =
        [| 2 .. n |]
        |> Array.Parallel.map (fun x -> (x, IsPrime x))
        |> Array.filter snd
        |> Array.Parallel.map fst

    let inline IsFactor x factor = x % factor = 0
    let trace x = printf "%O\n" x; x
    let inspect x =
        for y in x do
            printf "%O;" y
        printf "\n"
        x
    let inline max x y = if x > y then x else y
    let inline min x y = if x < y then x else y

let hasCommonDivider primes x y =
    let min = Euler.min x y
    primes 
    |> Set.exists (fun prime -> prime < min && Euler.IsFactor x prime && Euler.IsFactor y prime)

let countForNumerator primes (maxD: int) n =
    let upperBound = Euler.min maxD <| (3 * n) - 1
    let lowerBound = Euler.min maxD <| (2 * n) + 1
    // printf "for %i [| %i .. %i |]\n" n lowerBound upperBound
    if Set.contains n primes then
        Math.Abs(upperBound - lowerBound + 1)
    elif lowerBound = upperBound then 0
    else
        [| lowerBound .. upperBound |]
        |> Array.filter (fun d -> Set.contains d primes || not <| hasCommonDivider primes n d)
        |> Array.length

let coutingFractionsInRange maxD =
    let primes = Set.ofArray <| Euler.AllPrime maxD
    Array.Parallel.map (countForNumerator primes maxD) [| 2 .. (maxD / 2) - 1 |] // todo 
    |> Array.sum

[<EntryPoint>]
let main argv =
    let testResult = coutingFractionsInRange 8
    if  testResult = 3 then // expect 3
        let finalResult = coutingFractionsInRange 12_000
        if not <| Array.contains finalResult [| 7359499 |] then
            printf "%i\n" finalResult
        else
            printf "wrong answer: %i\n" finalResult
    else
        printf "%i code doesn't work for 8 won't work for 12 000\n" testResult
    0 // return an integer exit code
