let scores: Map<string,number>=new Map();
scores.set("Alice", 85);
scores.set("Bob", 92);  
scores.set("Charlie", 78);
scores.set("Diana", 88);
scores.set("Ethan", 95);
scores.set("Fiona", 82);
console.log(scores);
console.log(scores.get("Alice")); // 85
console.log(scores.get("86")); // 92
console.log(scores.has("Charlie")); // true
console.log(scores.has("George")); // false
for (let [name, score] of scores) {
    console.log(`${name}: ${score}`);
}
scores.delete("Diana");
console.log(scores.size); // 5  
scores.clear();
console.log(scores.size); // 0