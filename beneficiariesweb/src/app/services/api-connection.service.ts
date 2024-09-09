import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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
        console.error('Error obteniendo paises:', error);
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
        console.error('Error obteniendo empleados:', error);
        return throwError(() => new Error('Error obteniendo empleados'));
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
        console.error('Error obteniendo beneficiarios:', error);
        return throwError(() => new Error('Error obteniendo beneficiarios'));
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
        console.error('Error obteniendo beneficiarios:', error);
        return throwError(() => new Error('Error obteniendo beneficiarios'));
      })
    );
  }  
  
  createEmployee(employeRequest: EmployeeDTO): Observable<EmployeeDTO> {
    const url = `${this.baseUrl}/api/Employee/Add`;
    return this.http.post<EmployeeDTO>(url, employeRequest).pipe(
      catchError((error: any) => {
        console.error('Error creando empleado:', error);
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
      map(response => {
        return 'Empleado actualizado correctamente';
      }),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Error al actualizar empleado';
        if (error.error instanceof ErrorEvent) {
          console.error('An error occurred:', error.error.message);
        } else {
          console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
          if (error.status === 409) {  
            errorMessage = 'Error: El número de empleado ya existe.';
          }
        }
        return throwError(errorMessage);
      })
    );
  }
  
  deleteEmployee(id: number): Observable<string> {
    const url = `${this.baseUrl}/api/Employee/Delete/${id}`;
    return this.http.delete<string>(url, { responseType: 'text' as 'json' }).pipe(
      catchError((error: any) => {
        console.error('Error eliminando empleado:', error);
        return throwError(() => new Error('Error eliminando empleado: ' + (error.message || error)));
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
        console.error('Error obteniendo empleado:', error);
        return throwError(() => new Error('Error obteniendo empleado'));
      })
    );
  }  
  
  createBeneficiary(beneficiaryRequest: BeneficiaryDTO): Observable<BeneficiaryDTO> {
    const url = `${this.baseUrl}/api/Beneficiary/Add`;
    return this.http.post<BeneficiaryDTO>(url, beneficiaryRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Error creando beneficiario';
        if (error.error instanceof ErrorEvent) {
          console.error('An error occurred:', error.error.message);
        } else {
          console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
          if (error.status === 409) {
            errorMessage = 'La suma total de los porcentajes de participación no puede exceder el 100.';
          } else {
            errorMessage = `Error del servidor al crear beneficiario: ${error.message}`;
          }
        }
        return throwError(() => new Error(errorMessage));
      })
    );
  }
   
  updateBeneficiary(beneficiaryRequest: BeneficiaryDTO): Observable<string> {
    return this.http
    .put(`${this.baseUrl}/api/Beneficiary/Update`, beneficiaryRequest, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
    .pipe(
      map(response => {
        return 'Beneficiario actualizado correctamente';
      }),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Error al actualizar beneficiario';
        if (error.error instanceof ErrorEvent) {
          console.error('An error occurred:', error.error.message);
        } else {
          console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
        }
        return throwError(errorMessage);
      })
    );
  }
  
  deleteBeneficiary(id: number): Observable<string> {
    const url = `${this.baseUrl}/api/Beneficiary/Delete/${id}`;
    return this.http.delete<string>(url, { responseType: 'text' as 'json' }).pipe(
      catchError((error: any) => {
        console.error('Error eliminando beneficiario:', error);
        return throwError(() => new Error('Error eliminando beneficiario: ' + (error.message || error)));
      })
    ) 
  } 
  
  getBeneficiaryXId(
    beneficiaryId: number,
  ): Observable<BeneficiaryDTO> {
    const url = `${this.baseUrl}/api/Beneficiary/${beneficiaryId}`;
    
    return this.http.get<{result: BeneficiaryDTO}>(url).pipe(
      map(response => response.result), 
      catchError((error: any) => {
        console.error('Error obteniendo beneficiario:', error);
        return throwError(() => new Error('Error obteniendo beneficiario'));
      })
    );
  } 
  
  getEmployeesXFilter(searchTerm: string): Observable<EmployeeDTO[]> {
    const url = `${this.baseUrl}/api/Employee/ObtAllXFilter?term=${searchTerm}`;
    return this.http.get<EmployeeDTO[]>(url).pipe(
      catchError(error => {
        console.error('Error obteniendo empleados por filtro:', error);
        return throwError(() => new Error('Error obteniendo empleados por filtro'));
      })
    );
  }

  validateTotalParticipation(employeeId: number, newPercentaje: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/api/Beneficiary/ValidateParticipation/${employeeId}/${newPercentaje}`).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error validando la participación:', error);
        return throwError(() => new Error('Error al validar la participación'));
      })
    );
  }
}
