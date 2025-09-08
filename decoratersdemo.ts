function Logger(constructor: Function) {
    console.log(`Creating instance of:${constructor.name} `);
}
@Logger
class User {
    constructor(public name: string, public age: number) {}
}
console.log(`User Name: ${new User('Alice', 30).name}`);

