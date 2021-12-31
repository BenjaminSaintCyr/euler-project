namespace euler

module Euler =
    let IsPrime n =
        let sqrt' = (float >> sqrt >> int) n // square root of integer
        [ 2 .. sqrt' ] // all numbers from 2 to sqrt'
        |> List.forall (fun x -> n % x <> 0) // no divisors

    let AllPrime n =
        let choosePrime x = if IsPrime x then Some x else None
        Array.Parallel.choose choosePrime [| 2 .. n |]


    let inline IsFactor x factor = x % factor = 0

    let rec gcd a b =
        if b = 0 then
            abs a
        else
            gcd b (a % b)

    let highestPrime n = AllPrime n |> Seq.max

    let totient n = seq { 1 .. n - 1 } |> Seq.filter (gcd n >> (=) 1) |> Seq.length

module DBG =
    let trace x = printf "%O\n" x; x
    let inspect x =
        for y in x do
            printf "%O;" y
        printf "\n"
        x

    let benchmark fn x =
        let stopWatch = System.Diagnostics.Stopwatch.StartNew()
        let result = fn x
        stopWatch.Stop()
        printf "%f\n" stopWatch.Elapsed.TotalMilliseconds
        result
