class Trainer{
    name : string;
    experience : number;
    constructor(name: string, experience: number) {
        this.name = name;
        this.experience = experience;
        
    }
    introduce(): void {
        console.log(`Hello, my name is ${this.name} and I have ${this.experience} years of experience.`);
    }   
    calculatesessions(): number {
        return this.experience * 10; // Assuming 10 sessions per year of experience
    }
}
const trainer = new Trainer("John Doe", 20);
trainer.introduce();
console.log(`Total sessions: ${trainer.calculatesessions()}`);