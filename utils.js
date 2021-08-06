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

