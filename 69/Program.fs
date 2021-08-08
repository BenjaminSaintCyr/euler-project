open System

///////////////////////////////////////////////////////////////////////////////
//                   map reduce version         //
////////////////////////////////////////////////////////
     
let inline isFactor x y = x % y = 0

let isPrime n =
  let sqrt' = (float >> sqrt >> int) n // square root of integer
  [ 2 .. sqrt' ] // all numbers from 2 to sqrt'
  |> List.forall (fun x -> n % x <> 0) // no divisors

let allPrimes =
  let rec allPrimes' n =
    seq { // sequences are lazy, so we can make them infinite
      if isPrime n then
        yield n
      yield! allPrimes' (n+1) // recursing
    }
  allPrimes' 2 // starting from 2

let allPrime n =
    [| 2 .. n |]
    |> Array.Parallel.map (fun x -> (x, isPrime x))
    |> Array.filter snd
    |> Array.Parallel.map fst

let trace x = printf "%O\n" x; x

let totient primes x =
    let mutable relPrimes = x - 1; // exclude itself
    let factors = Set.filter (fun prime -> x > prime && isFactor x prime) primes
    for primeNumber in factors do
        let nbFactors = 
            let initialFactors = x / primeNumber
            Set.filter (fun prime -> primeNumber > prime && isFactor initialFactors prime) factors
            |> Set.fold (/) initialFactors
            |> (fun x -> if x > 1 then x - 1 else x)

        let relPrimes' = relPrimes - nbFactors // exclue when prime * n = x
        if relPrimes' <= 0 then printf "!relPrimes! from %i to %i with %i and %i\n" relPrimes relPrimes' x primeNumber
        relPrimes <- relPrimes'
    relPrimes

let totientRatio primes x = x / (totient primes x)


let maxTotientRatioUnder n =
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fastPrimes = Set.ofArray <| allPrime n
    stopWatch.Stop()
    printf "primes time %f\n" stopWatch.Elapsed.TotalMilliseconds
    [| 2 .. n |]
    |> Array.Parallel.map (fun x -> (x, (totientRatio fastPrimes x))) 
    |> Array.maxBy snd
    |> fst


[<EntryPoint>]
let main argv =
    printf "/////////////////////////////////////////////////////////////////////////////// Begining\n"
    try
        let TestTable = [
            (2, 1)
            (3, 2)
            (4, 2)
            (5, 4)
            (6, 2)
            (7, 6)
            (8, 4)
            (9, 6)
            (10,4)
        ]
        let isTestOk fn (value, expected) =
            let result = fn value
            let passed = result = expected
            if not passed then printf "[FAIL] expected %i, but got %i for value %i\n" expected result value
            else printf "[OK] for %i\n" value
            passed
        let areAllTestOk =
            let testPrimes = Set.ofArray <| allPrime 10
            TestTable
            |> List.map (isTestOk (totient testPrimes))
            |> List.reduce (&&)
        let benchmark fn x =
            let stopWatch = System.Diagnostics.Stopwatch.StartNew()
            let result = fn x
            stopWatch.Stop()
            printf "%f\n" stopWatch.Elapsed.TotalMilliseconds
            result
        if areAllTestOk then
            let load = 1_000_000
            printf "%i\n" <| benchmark maxTotientRatioUnder load
    with e ->
        printf "error with program %O %O \n"  e.Data e.Message
    printf "/////////////////////////////////////////////////////////////////////////////// End\n"
    0// return an integer exit code
