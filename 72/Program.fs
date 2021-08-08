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

    let SequentialPrimes n =
        Array.fold (fun primes x -> if Set.exists (IsFactor x) primes then primes else primes.Add(x) ) (Set.empty.Add 2) [| 3 .. n |]

    let trace x = printf "%O\n" x; x
    let inspect x =
        for y in x do
            printf "%O;" y
        printf "\n"
        x
    let inline max x y = if x > y then x else y
    let inline min x y = if x < y then x else y

    let inline Factorise primes x = Set.filter (IsFactor x) primes

let totient primes d =
    if Set.contains d primes then int64 <| d - 1
    else
        Euler.Factorise primes d
        |> Set.map (float >> (fun x -> (1.0 - (1.0 / x))))
        |> Set.fold (*) (float d)
        |> int64

let coutingFractionsInRange maxD =
    let primes = Set.ofArray <| Euler.AllPrime maxD
    Array.Parallel.map (totient primes) [| 2 .. maxD |]
    |> Array.sum

[<EntryPoint>]
let main argv =
    let result = coutingFractionsInRange 8
    if result = (int64 21) then
        printf "%i\n" <| coutingFractionsInRange 1_000_000
        // 303963552391
    else
        printf "Fail with %i\n" result
    0 // return an integer exit code
