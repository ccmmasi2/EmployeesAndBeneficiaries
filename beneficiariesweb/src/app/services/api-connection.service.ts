import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BeneficiaryDTO } from '@app/models/beneficiary.model';
import { CountryDTO } from '@app/models/country.model';
import { EmployeeDTO } from '@app/models/employee.model';
import { environment } from 'enviroment/enviroment';
import { catchError, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ApiConnectionService {
  private baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getCountries(): Observable<CountryDTO[]> {
    const url = `${this.baseUrl}/api/Country/ObtAll`;
    return this.http.get<CountryDTO[]>(url).pipe(
        catchError((error: any) => {
          console.error('Error getting all countries:', error);
          return [];
        })
    );
  }

  getEmployees(
    page: number,
    sizePage: number,
    sorting: string
  ): Observable<{ totalRecords: number, currentPage: number, sizePage: number, sorting: number, data: EmployeeDTO[] }> {
    let url = `${this.baseUrl}/api/Employee/ObtAll?page=${page}&sizePage=${sizePage}`;
   
    if(sorting) {
      url += `&sorting=${sorting}`;
    }

    return this.http.get<any>(url).pipe(
      map((response: any) => {
        return {
          currentPage: response.page,
          sizePage: response.pageSize,
          sorting: response.sorting,
          totalRecords: response.totalCount,
          data: response.data
        };
      }),
      catchError((error: any) => {
        console.error('Error getting all employees:', error);
        return throwError(() => new Error('Failed to load employees'));
      })
    );
  } 

  getBeneficiaries(
    page: number,
    sizePage: number,
    sorting: string
  ): Observable<{ totalRecords: number, currentPage: number, sizePage: number, sorting: number, data: BeneficiaryDTO[] }> {
    let url = `${this.baseUrl}/api/Beneficiary/ObtAll?page=${page}&sizePage=${sizePage}`;

    if(sorting) {
      url += `&sorting=${sorting}`;
    }

    return this.http.get<any>(url).pipe(
      map((response: any) => {
        return {
          currentPage: response.page,
          sizePage: response.pageSize,
          sorting: response.sorting,
          totalRecords: response.totalCount,
          data: response.data
        };
      }),
      catchError((error: any) => {
        console.error('Error getting all beneficiaries:', error);
        return throwError(() => new Error('Failed to load beneficiaries'));
      })
    );
  } 

  getBeneficiariesByEmployeeId(
    employeeId: number,
    page: number,
    sizePage: number,
    sorting: string
  ): Observable<{ totalRecords: number, currentPage: number, sizePage: number, sorting: number, data: BeneficiaryDTO[] }> {
    let url = `${this.baseUrl}/api/Beneficiary/ObtAllXEmployeeId?employeeId=${employeeId}&page=${page}&sizePage=${sizePage}`;
   
    if(sorting) {
      url += `&sorting=${sorting}`;
    }

    return this.http.get<any>(url).pipe(
      map((response: any) => {
        return {
          currentPage: response.page,
          sizePage: response.pageSize,
          sorting: response.sorting,
          totalRecords: response.totalCount,
          data: response.data
        };
      }),
      catchError((error: any) => {
        console.error('Error getting all beneficiaries:', error);
        return throwError(() => new Error('Failed to load beneficiaries'));
      })
    );
  }  

  createEmployee(employeRequest: EmployeeDTO): Observable<EmployeeDTO> {
    let url = `${this.baseUrl}/api/Employee/Add`;
    return this.http.post<EmployeeDTO>(url, employeRequest).pipe(
        catchError((error: any) => {
          console.error('Error creating employee:', error);
          throw error;  
        })
      )
  }

  getEmployeeXId(
    employeeId: number,
  ): Observable<EmployeeDTO> {
    const url = `${this.baseUrl}/api/Employee/${employeeId}`;

    return this.http.get<{result: EmployeeDTO}>(url).pipe(
      map(response => response.result), 
      catchError((error: any) => {
        console.error('Error getting the employee:', error);
        return throwError(() => new Error('Failed to load the employee'));
      })
    );
  } 
}
