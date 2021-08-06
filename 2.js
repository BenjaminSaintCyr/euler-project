(() => {
    let previous = 1;
    let current = 2;
    let count = 0;
    const maxvalue = 4000000;
    while (current < maxvalue ) {
        // sum of even-valued terms
        if (current % 2 === 0) {
            count += current;
        }
        // adding previous two terms
        let next = previous + current; 
        // rotating
        previous = current; 
        current = next;

    }
    return count;
})();
