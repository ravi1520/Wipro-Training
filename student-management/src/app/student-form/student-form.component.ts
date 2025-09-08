import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Student } from '../student.model';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss']
})
export class StudentFormComponent implements OnChanges {
  @Input() editing: Student | null = null;
  @Output() done = new EventEmitter<void>();

  constructor(private fb: FormBuilder, private svc: StudentService) {}

  form = this.fb.group({
    id: [0],
    name: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    course: ['', Validators.required],
    year: [1, [Validators.required, Validators.min(1), Validators.max(4)]],
  });

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['editing']) {
      if (this.editing) this.form.reset(this.editing);
      else this.form.reset({ id: 0, name: '', email: '', course: '', year: 1 });
    }
  }

  submit() {
    if (this.form.invalid) return;
    const value = this.form.value as Student;
    if (value.id && value.id !== 0) this.svc.update(value);
    else { const { id, ...create } = value; this.svc.add(create as any); }
    this.done.emit();
    this.form.reset({ id: 0, name: '', email: '', course: '', year: 1 });
  }

  cancel() { this.done.emit(); this.form.reset({ id: 0, name: '', email: '', course: '', year: 1 }); }

  get f() { return this.form.controls; }
}
