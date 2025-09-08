var scores = new Map();
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
for (var _i = 0, scores_1 = scores; _i < scores_1.length; _i++) {
    var _a = scores_1[_i], name_1 = _a[0], score = _a[1];
    console.log("".concat(name_1, ": ").concat(score));
}
scores.delete("Diana");
console.log(scores.size); // 5  
scores.clear();
console.log(scores.size); // 0
