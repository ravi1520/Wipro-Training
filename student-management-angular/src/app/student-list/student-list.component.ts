import { Component } from '@angular/core';
import { StudentService } from '../student.service';
import { Student } from '../student.model';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html'
})
export class StudentListComponent {
  students: Student[] = [];
  selectedStudent: Student | null = null;

  constructor(private studentService: StudentService) {
    this.loadStudents();
  }

  loadStudents() {
    this.students = this.studentService.getStudents();
  }

  editStudent(student: Student) {
    this.selectedStudent = { ...student };
  }

  deleteStudent(id: number) {
    this.studentService.deleteStudent(id);
    this.loadStudents();
  }

  onSave(student: Student) {
    if (student.id) {
      this.studentService.updateStudent(student);
    } else {
      this.studentService.addStudent(student);
    }
    this.selectedStudent = null;
    this.loadStudents();
  }
}
