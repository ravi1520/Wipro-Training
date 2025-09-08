const fruits = ['apple', 'banana', 'cherry'];
for (const fruit of fruits) {
    console.log(fruit);
}
//traverse a map using  for of loop
const scores1 = new Map([
    ['Alice', 85],
    ['Bob', 92],
    ['Charlie', 78]
]);
for (const [name, score] of scores1) {
    console.log(`${name}: ${score}`);
}
//traverse a set using for of loop
const uniqueNumbers = new Set([1, 2, 3, 4, 5]);
for (const number of uniqueNumbers) {
    console.log(number);
}
