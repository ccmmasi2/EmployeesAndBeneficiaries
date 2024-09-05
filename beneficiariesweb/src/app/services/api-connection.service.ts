import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BeneficiaryDTO } from '@app/models/beneficiary.model';
import { CountryDTO } from '@app/models/country.model';
import { EmployeeDTO } from '@app/models/employee.model';
import { environment } from 'enviroment/enviroment';
import { catchError, Observable } from 'rxjs';

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

  getEmployees(): Observable<EmployeeDTO[]> {
    const url = `${this.baseUrl}/api/Employee/ObtAll`;
    return this.http.get<EmployeeDTO[]>(url).pipe(
        catchError((error: any) => {
          console.error('Error getting all Employees:', error);
          return [];
        })
    );
  }

  getBeneficiariesByEmployeeId(
    employeeId: number
  ): Observable<BeneficiaryDTO[]> {
    const url = `${this.baseUrl}/api/Beneficiary/ObtAllXEmployeeId?categoryId=${employeeId}`;
    return this.http.get<BeneficiaryDTO[]>(url).pipe(
        catchError((error: any) => {
          console.error('Error getting all Beneficiaries by Employee Id:', error);
          return [];
        })
    );
  }  
}
