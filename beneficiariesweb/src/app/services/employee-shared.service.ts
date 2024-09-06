import { Injectable } from '@angular/core';
import { ApiConnectionService } from './api-connection.service';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { EmployeeDTO } from '@app/models/employee.model';
import { BeneficiaryDTO } from '@app/models/beneficiary.model';

@Injectable({
  providedIn: 'root'
})

export class EmployeeSharedService {
  private employeesSource = new BehaviorSubject<{ totalRecords: number, data: EmployeeDTO[] }>({ totalRecords: 0, data: [] });
  employees$ = this.employeesSource.asObservable();
  
  private beneficiarySource = new BehaviorSubject<{ totalRecords: number, data: BeneficiaryDTO[] }>({ totalRecords: 0, data: [] });
  beneficiaries$ = this.beneficiarySource.asObservable();

  constructor(private apiService: ApiConnectionService) {}

  getEmployees(page: number = 1, sizePage: number = 10, sorting: string = ''): void {
    this.apiService.getEmployees(page, sizePage, sorting).subscribe({
      next:  response => this.employeesSource.next({ totalRecords: response.totalRecords, data: response.data }),
      error: error => console.error('Error loading employees', error)
    });
  } 

  getBeneficiaries(page: number = 1, sizePage: number = 10, sorting: string = ''): void {
    this.apiService.getBeneficiaries(page, sizePage, sorting).subscribe({
      next:  response => this.beneficiarySource.next({ totalRecords: response.totalRecords, data: response.data }),
      error: error => console.error('Error loading beneficiaries', error)
    });
  } 

  getBeneficiariesByEmployeeId(employeeId: number, page: number = 1, sizePage: number = 10, sorting: string = ''): void {
    this.apiService.getBeneficiariesByEmployeeId(employeeId, page, sizePage, sorting).subscribe({
      next:  response => this.beneficiarySource.next({ totalRecords: response.totalRecords, data: response.data }),
      error: error => console.error('Error loading beneficiaries', error)
    });
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