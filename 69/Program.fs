open System

///////////////////////////////////////////////////////////////////////////////
//                   map reduce version         //
////////////////////////////////////////////////////////
     
let inline isFactor x factor = x % factor = 0

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

let calculateRelativePrimes x factors relPrimes primeFactor =
    let nbFactors =
        let initialFactors = x / primeFactor
        Set.filter (fun prime -> primeFactor > prime && isFactor initialFactors prime) factors // get factors of generated factors to remove duplicate factors
        |> Set.fold (/) initialFactors // remove duplicate factors: ex 6 is both a factor of 2 and 3 for ex 12
    let relPrimes' = relPrimes - nbFactors // exclue when prime * n = x
    if relPrimes' <= 0 then printf "!relPrimes! from %i to %i with %i and %i\n" relPrimes relPrimes' x primeFactor
    relPrimes'


let totient primes x =
    if Set.contains x primes then x - 1
    else
        let factors = Set.filter (fun prime -> x > prime && isFactor x prime) primes
        Set.fold (calculateRelativePrimes x factors) x factors

let totientRatio primes (x: int): float = (float x) / (float (totient primes x))


let maxTotientRatioUnder n =
    let fastPrimes = Set.ofArray <| allPrime n
    [| 2 .. n |]
    |> Array.Parallel.map (fun x -> (x, (totientRatio fastPrimes x))) 
    |> Array.maxBy snd


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
            printf "n:%O phi:%O\n" <|| maxTotientRatioUnder load
            // for x in maxTotientRatioUnder load do
                // printf "n:%O phi:%O\n" <|| x
    with e ->
        printf "error with program %O %O \n"  e.Data e.Message
    printf "/////////////////////////////////////////////////////////////////////////////// End\n"
    0// return an integer exit code
