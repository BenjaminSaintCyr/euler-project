// function getNDigits (x) {
//     let nDigits = 0;
//     for (let i = 0; x % (10 ** i) !== x; i++) {
//         nDigits = i + 1;
//     }
//     return nDigits;
// }

// function base10Ignore (x, firstDigits) {
//     return Math.floor(x / (10 ** firstDigits));   
// }
// function base10Focus (x, firstDigits) {
//     return x % (10 ** firstDigits);
// }

// function isPalindrome(x) {
//     if (x < 10) return true;
//     const nDigits = getNDigits(x);
//     for(let i = 0; i < nDigits; i++) {
//         let start = base10Ignore(base10Focus(x, (i + 1)), i);
//         let end = base10Focus(base10Ignore(x, nDigits - i - 1), i === 0 ? i : i + 1);
//         if (start != end) {
//             return false;
//         }
//     }
//     return true;
// }

function isPalindrome(x) {
    return parseInt(x.toString().split("").reverse().join("")) === x;
}

function maxPalindrome(digits) {
    const maxDigit = (10 ** digits) - 1;
    let largest = 0;
    for (let i = maxDigit; i > 0; i--) {
        for (let j = maxDigit; j > 0; j--) { // hmmm le typo !!! (stop using fucking for loops)
            const product = i * j
            if (isPalindrome(product) && product > largest) {
                largest = product;
            }
        }
    }
    return largest;
}

// not 90909 from 91 * 999
console.log(maxPalindrome(2));
console.log(maxPalindrome(3));
// 906609 TODO figure what's wrong
