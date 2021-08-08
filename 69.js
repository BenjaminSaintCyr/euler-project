
let primesNumbers = new Set();

///////////////////////////////////////////////////////////////////////////////
//                                   Utils                                   //
///////////////////////////////////////////////////////////////////////////////

var debugging = false;

function inspect(...args) {
    if (debugging) {
        console.log(...args);
    }
}

function handleBadCase(fn, result, expected, ...values) {
    debugging = true;
    console.log("was expecting ", expected, " but got ", result, " for ", ...values, "<=================");
    console.log("Env:", primesNumbers);
    fn(...values); // inspect exec
    debugging = false;
}

function TDD() {
    let td = [
        [2, 1],
        [3, 2],
        [4, 2],
        [5, 4],
        [6, 2],
        [7, 6],
        [8, 4],
        [9, 6],
        [10,4],
    ];
    for (const [value, expected] of td) {
        const result = totient(value);
        if (result !== expected) {
            handleBadCase(totient, result, expected, value);
            return;
        }
    }
}

///////////////////////////////////////////////////////////////////////////////
//                                    algo                                   //
///////////////////////////////////////////////////////////////////////////////


function maxFactor(x) { return Math.round(x / 2); }

function anyCommonFactor(x, y, factors) {
    if (x < y) throw "x should be bigger than y to work";
    if (x % y === 0) {
        inspect(`common factor of ${x} and ${y} is ${y}`);
        return true;
    }
    if (primesNumbers.has(y)) {
        inspect(`No common factor ${y} is prime `, primesNumbers);
        return false;
    }
    let maxCommonFactor = maxFactor(y);
    for (const factor of factors) {
        if (y % factor  === 0) {
            inspect(`common factor of ${x} and ${y} is ${factor}`);
            return true;
        }
    }
    inspect(`no match below`, maxCommonFactor );
    return false;
}

// function totient (x) {
//     const initialPrimes = 1;
//     let primes = initialPrimes; // case 1
//     let maxSelfFactor = maxFactor(x);
//     let factors = [];
//     for (let i = 2; i < x; i ++) {
//         let result = anyCommonFactor(x, i, factors);
//         if (result) {
//             factors.push(i);
//         }
//         inspect("anyCommonFactor", x, i, result);
//         if (!result) {
//             ++primes;
//             inspect("++", primes);
//         }
//         if (i === maxSelfFactor && primes === maxSelfFactor) { // every number below is primes to him
//             inspect(x, "is prime!", initialPrimes, primes, maxSelfFactor);
//             primesNumbers.add(x);
//         }
//     }
//     return primes;
// }

function rangeTotient (x) {
    const initialPrimes = 1;
    let primes = initialPrimes; // case 1
    let maxSelfFactor = maxFactor(x);
    let factors = [];
    for (let i = 2; i < x; i ++) {
        let result = anyCommonFactor(x, i, factors);
        if (result) {
            factors.push(i);
        }
        inspect("anyCommonFactor", x, i, result);
        if (!result) {
            ++primes;
            inspect("++", primes);
        }
        if (i === maxSelfFactor && primes === maxSelfFactor) { // every number below is primes to him
            inspect(x, "is prime!", initialPrimes, primes, maxSelfFactor);
            primesNumbers.add(x);
        }
    }
    return primes;
}

function totientRation (x) {
    return x / rangeTotient(x);
}

function maxTotientRatioUnder (n) {
    let maxIndex = 0;
    let maxTotientRatio = 0;
    for (let i = 1; i <= n; i++) {
        const currentTotientRatio = totientRation(i);
        if (currentTotientRatio  > maxTotientRatio) {
            maxTotientRatio = currentTotientRatio;
            maxIndex = i;
        }
    }
    return maxIndex;
}

TDD();
// debugging = true;
console.log(maxTotientRatioUnder(10));
console.log(maxTotientRatioUnder(1000000));
