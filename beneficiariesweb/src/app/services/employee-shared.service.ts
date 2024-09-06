import { Injectable } from '@angular/core';
import { ApiConnectionService } from './api-connection.service';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { EmployeeDTO } from '@app/models/employee.model';

@Injectable({
  providedIn: 'root'
})

export class EmployeeSharedService {
  private employeesSource = new BehaviorSubject<EmployeeDTO[]>([]);
  employees$ = this.employeesSource.asObservable();

  constructor(private apiService: ApiConnectionService) {
  }

  getEmployees(): void {
    this.apiService.getEmployees().subscribe(
      employees => this.employeesSource.next(employees),
      error => console.error('Error loading employees', error)
    );
  }

  addEmployee(employee: EmployeeDTO): Observable<EmployeeDTO>{
    return this.apiService.createEmployee(employee).pipe(
      catchError(error => {
        console.error('Error adding employee', error);
        return throwError(() => new Error('Failed to add employee'));
      })
    );
  }
}