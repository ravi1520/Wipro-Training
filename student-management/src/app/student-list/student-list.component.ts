import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from '../student.service';
import { Student } from '../student.model';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent {
  students$ = this.svc.students$;
  @Output() edit = new EventEmitter<Student>();

  constructor(private svc: StudentService) {}

  onEdit(s: Student) { this.edit.emit(s); }
  onDelete(id: number) {
    if (confirm('Delete this student?')) { this.svc.delete(id); }
  }
}
