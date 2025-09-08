import { Injectable } from '@angular/core';
import { Student } from './student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private students: Student[] = [
    { id: 1, name: 'John Doe', age: 20, course: 'Math' },
    { id: 2, name: 'Jane Smith', age: 22, course: 'Science' }
  ];

  getStudents(): Student[] {
    return this.students;
  }

  addStudent(student: Student) {
    student.id = this.students.length + 1;
    this.students.push(student);
  }

  updateStudent(updatedStudent: Student) {
    const index = this.students.findIndex(s => s.id === updatedStudent.id);
    if (index > -1) {
      this.students[index] = updatedStudent;
    }
  }

  deleteStudent(id: number) {
    this.students = this.students.filter(s => s.id !== id);
  }
}
