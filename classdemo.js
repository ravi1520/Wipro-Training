var Trainer = /** @class */ (function () {
    function Trainer(name, experience) {
        this.name = name;
        this.experience = experience;
    }
    Trainer.prototype.introduce = function () {
        console.log("Hello, my name is ".concat(this.name, " and I have ").concat(this.experience, " years of experience."));
    };
    Trainer.prototype.calculatesessions = function () {
        return this.experience * 10; // Assuming 10 sessions per year of experience
    };
    return Trainer;
}());
var trainer = new Trainer("John Doe", 20);
trainer.introduce();
console.log("Total sessions: ".concat(trainer.calculatesessions()));
