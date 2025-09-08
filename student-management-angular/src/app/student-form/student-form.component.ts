import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Student } from '../student.model';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html'
})
export class StudentFormComponent {
  @Input() student: Student | null = null;
  @Output() save = new EventEmitter<Student>();

  tempStudent: Student = { id: 0, name: '', age: 0, course: '' };

  ngOnChanges() {
    this.tempStudent = this.student ? { ...this.student } : { id: 0, name: '', age: 0, course: '' };
  }

  onSubmit() {
    this.save.emit(this.tempStudent);
    this.tempStudent = { id: 0, name: '', age: 0, course: '' };
  }
}
