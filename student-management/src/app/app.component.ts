import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Student } from './student.model';
import { StudentListComponent } from './student-list/student-list.component';
import { StudentFormComponent } from './student-form/student-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, StudentListComponent, StudentFormComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Student Management System';
  editing: Student | null = null;
  startEdit(s: Student) { this.editing = { ...s }; }
  stopEdit() { this.editing = null; }
}
