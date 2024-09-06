import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CountryDTO } from '@app/models/country.model';
import { EmployeeDTO } from '@app/models/employee.model';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';
import { EmployeeSharedService } from '@app/services/employee-shared.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
})

export class EmployeeFormComponent implements OnInit {
  isCollapsed: boolean = true;

  @ViewChild('employeeForm') employeeForm!: NgForm; 
  cartItems: EmployeeDTO[] = []; 

  selectCountryId: number = 0;
  countryIdOptions: CountryDTO[] = [];
  name: string = '';
  lastName: string = '';
  birthDay!: Date;
  phoneNumber: string = '';
  curp: string = '';
  ssn: string = '';
  employeeNumber: number = 0;
  
  constructor(
    public apiConnectionService: ApiConnectionService,
    private alertService: AlertService,
    public employeeSharedService: EmployeeSharedService
  ) {
  }

  ngOnInit(): void {
    this.loadDataOptions(); 
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;   
  }

  loadDataOptions(): void {
    this.apiConnectionService.getCountries().subscribe((info) => {
      if(info){
        this.countryIdOptions = info;
      }
      else {
        const message = `Error loading countries`
        this.alertService.showAlert(message, 'error'); 
      }
    },
    (error) => {
      const message = `Error loading countries: "${error}"`
      this.alertService.showAlert(message, 'error'); 
    })
  }  

  validateAge(birthDay: Date | string): boolean {
    if (typeof birthDay === 'string') {
        birthDay = new Date(birthDay);
    }
    
    if (!(birthDay instanceof Date) || isNaN(birthDay.getTime())) {
        return false;
    }

    const currentDate = new Date();
    const diff = currentDate.getTime() - birthDay.getTime();
    const age = Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
    return age >= 18;
  }

  submitForm(): void {
    if (this.employeeForm.valid) {
      if (!this.validateAge(this.birthDay)) {
        const message = `The Employee must be over than 18 years old`
        this.alertService.showAlert(message, 'error'); 
        this.isCollapsed = true;
        return;
      }
      else {
        const employeeRequest: EmployeeDTO = this.prepareEmployeeDTO();
  
        this.employeeSharedService.addEmployee(employeeRequest).subscribe({
          next: () => {
            const message = 'Employee created successfully';
            this.alertService.showAlert(message, 'success');
            this.resetForm();
            this.refreshEmployeeList();
          },
          error: (error) => {
            const message = `An error occurred while creating the employee: ${error.message || error}`;
            this.alertService.showAlert(message, 'error');
            this.isCollapsed = true;
          }
        });
      } 
    }  else {
      this.alertService.showAlert('Please fill in all required fields.', 'error');
      this.isCollapsed = true;
    }
  }
  
  refreshEmployeeList() {
    this.employeeSharedService.getEmployees();
  }

  private prepareEmployeeDTO(): EmployeeDTO {
    const employeeRequest: EmployeeDTO = {
      id: 0,
      countryId: this.selectCountryId, 
      employeeNumber: this.employeeNumber,
      name: this.name,
      lastName: this.lastName, 
      birthDay: this.birthDay,
      curp: this.curp,
      ssn: this.ssn,
      phoneNumber: this.phoneNumber 
    }

    return employeeRequest;
  }

  private resetForm(): void {
    this.selectCountryId = 0;
    this.name = '';
    this.lastName = '';
    this.birthDay = null!;
    this.phoneNumber = '';
    this.curp = '';
    this.ssn = '';
    this.employeeNumber = 0;
    this.isCollapsed = true;

    this.employeeForm.resetForm();
  }
}
