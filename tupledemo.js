var person = ["Alice", 30];
var coordinates = [10, 20];
function displayTuple(tuple) {
    console.log("Name: ".concat(tuple[0], ", Age: ").concat(tuple[1]));
}
displayTuple(person); // Output: Name: Alice, Age: 30
console.log("Coordinates: (".concat(coordinates[0], ", ").concat(coordinates[1], ")"));
