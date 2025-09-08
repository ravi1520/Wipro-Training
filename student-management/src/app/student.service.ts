import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Student } from './student.model';

@Injectable({ providedIn: 'root' })
export class StudentService {
  private _students = new BehaviorSubject<Student[]>([
    { id: 1, name: 'Aarav Sharma', email: 'aarav@example.com', course: 'Computer Science', year: 2 },
    { id: 2, name: 'Ishita Nair', email: 'ishita@example.com', course: 'Electronics', year: 3 },
    { id: 3, name: 'Rahul Mehta', email: 'rahul@example.com', course: 'Mechanical', year: 1 },
  ]);

  readonly students$ = this._students.asObservable();

  private nextId(): number {
    const list = this._students.getValue();
    return list.length ? Math.max(...list.map(s => s.id)) + 1 : 1;
  }

  add(student: Omit<Student, 'id'>) {
    const list = this._students.getValue();
    const newStudent: Student = { id: this.nextId(), ...student };
    this._students.next([...list, newStudent]);
  }

  update(updated: Student) {
    const list = this._students.getValue();
    const idx = list.findIndex(s => s.id === updated.id);
    if (idx >= 0) {
      const copy = [...list];
      copy[idx] = { ...updated };
      this._students.next(copy);
    }
  }

  delete(id: number) {
    const list = this._students.getValue();
    this._students.next(list.filter(s => s.id != id));
  }

  getById(id: number) {
    return this._students.getValue().find(s => s.id === id);
  }
}
