let isPrime(x, primes) { // logn 
    for (const prime of primes) {
        if (x % prime === 0) {
            return false;
        }
    }
    return true;
}

let primesBottomUp(x) { // nlogn (logn space)
    let primes = new Set([2]);
    for (let i = 3; i < x; i++) {
        if (isPrime(i, primes)) {
            primes.add(i);
        }
    }
    return primes;
}

let inline isFactor x y =
    x % y === 0;

let primeFactors x = // nlogn
    let largestFactor = 0;
    let primes = primesBottomUp(x); // nlogn
    for (let i = 0; i < x; i++) {
        if (primes.has(i) && isFactor(x, i)) { // nlog(n)
            largestFactor = i;
        }
    }
    return largestFactor ;

console.log(primeFactors(600851475143));
