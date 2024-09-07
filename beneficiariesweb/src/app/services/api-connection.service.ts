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
    const url = `${this.baseUrl}/api/Employee/Add`;
    return this.http.post<EmployeeDTO>(url, employeRequest).pipe(
        catchError((error: any) => {
          console.error('Error creating employee:', error);
          throw error;  
        })
      )
  } 

  updateEmployee(employeRequest: EmployeeDTO): Observable<string> {
    return this.http
      .put(`${this.baseUrl}/api/Employee/Update`, employeRequest, {
        observe: 'response',
        responseType: 'text' as 'json'
      })
      .pipe(
        map((response) => {
          if (response.status === 200) {
            return response.body ? response.body.toString() : "Empleado actualizado correctamente.";
          } else if (response.status === 204) {
            return "Actualización exitosa, sin contenido para mostrar.";
          } else {
            return 'Error desconocido';
          }
        }),
        catchError((error) => {
          console.error('Error updating employee:', error);
          return 'Error al actualizar empleado';  
        })
      );
  } 

  deleteEmployee(id: number): Observable<string> {
    const url = `${this.baseUrl}/api/Employee/Delete/${id}`;
    return this.http.delete<string>(url, { responseType: 'text' as 'json' }).pipe(
        catchError((error: any) => {
          console.error('Error deleting  employee:', error);
          return throwError(() => new Error('Error deleting employee: ' + (error.message || error)));
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
