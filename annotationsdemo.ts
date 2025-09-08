// interface Person{
//     name: string;

// }
// interface Employee extends Person {
//     employeeId: number;
   
// }
// const alices: Employee = {
//     name: "Alice",    
//     employeeId: 12345
// };
// console.log('Employee Name:', alices.name);
// console.log('Employee ID:', alices.employeeId);
interface Student{
    name: string;
    
    
}
 interface Graduate extends Student {
    graduationYear: number;
}
const Ravi: Graduate = {
    name: "Ravi",       
    graduationYear: 2023
};
console.log('Graduate Name:', Ravi.name);
console.log('Graduation Year:', Ravi.graduationYear);