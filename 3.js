function isPrime(x) {
    for (let i = 2; i < x; i++) {
        if (x % i === 0) {
            return false;
        }
    }
    return true;
}


// function isPrime(x, primes) { // logn 
//     for (const prime of primes) {
//         if (x % prime === 0) {
//             return false;
//         }
//     }
//     return true;
// }

// function primesBottomUp(x) { // nlogn (logn space)
//     let primes = new Set([2]);
//     for (let i = 3; i < x; i++) {
//         if (isPrime(i, primes)) {
//             primes.add(i);
//         }
//     }
//     return primes;
// }

function isFactor(x, y) {
    return x % y === 0;
}

function primeFactors(x) { // n*n (worst case)
    for (let i = 2; i < x; i++) { 
        if (isFactor(x, i) && isPrime(x / i)) { // (O(1) + n)
            return x / i;
        }
    }
    return -1; // fail
}

// console.log(primeFactors(13195));
console.log(primeFactors(600851475143));
