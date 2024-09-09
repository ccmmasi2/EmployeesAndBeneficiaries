import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { CountryDTO } from '@app/models/country.model';
import { EmployeeDTO } from '@app/models/employee.model';
import { ConfirmDialogComponent } from '@app/modules/shared/confirm-dialog/confirm-dialog.component';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';
import { ReactiveSharedService } from '@app/services/reactive-shared.service';
import { EventService } from '@app/services/event.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
})

export class EmployeeFormComponent implements OnInit {
  isCollapsed: boolean = true;
 
  @ViewChild('employeeForm') employeeForm!: NgForm; 
  cartItems: EmployeeDTO[] = []; 

  employeeId: number = 0;
  selectCountryId: number = 0;
  countryIdOptions: CountryDTO[] = [];
  name: string = '';
  lastName: string = '';
  birthDay: string = '';
  phoneNumber: string = '';
  curp: string = '';
  ssn: string = '';
  employeeNumber: number = 0;
  activateSubmitButton: boolean = true;
  
  constructor(
    public apiConnectionService: ApiConnectionService,
    private alertService: AlertService,
    public reactiveSharedService: ReactiveSharedService,
    private eventService: EventService,
    private dialog: MatDialog,
    private router: Router
  ) {
    this.eventService.watchButtonClick.subscribe((employeeId: number) => {
      this.loadEmployee(employeeId);
      this.employeeId = 0;
      this.activateSubmitButton = false;
    });
    this.eventService.editButtonClick.subscribe((employeeId: number) => {
      this.loadEmployee(employeeId);
      this.employeeId = employeeId;
      this.activateSubmitButton = true;      
    });
    this.eventService.deleteButtonClick.subscribe((employeeId: number) => {
      this.deleteEmployee(employeeId);
    });
    this.eventService.watchBeneficiariesButtonClick.subscribe((employeeId: number) => {
      this.router.navigate(['system/beneficiaries/',  { employeeId: employeeId } ]);
    });
  }

  loadEmployee(employeeId: number){
    this.apiConnectionService.getEmployeeXId(employeeId)
    .subscribe((employee) => { 
      employee.birthDay = new Date(employee.birthDay).toISOString().split('T')[0];  
      this.employeeForm.reset(employee);
      this.selectCountryId = employee.countryId;
      this.isCollapsed = false; 
    });
  }  

  deleteEmployee(employeeId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px'
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiConnectionService.deleteEmployee(employeeId).subscribe(
          (response) => { 
            this.alertService.showAlert(response, 'success');
            this.refreshEmployeeList();
          },
          (error) => {
            this.alertService.showAlert(`Error eliminando el empleado: ${error.message || error}`, 'error');
          }
        );
      }
    });
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
        const message = `Error cargando paises`
        this.alertService.showAlert(message, 'error'); 
      }
    },
    (error) => {
      const message = `Error cargando paises: "${error}"`
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
        const message = `El empleado debe tener al menos 18 años`
        this.alertService.showAlert(message, 'error'); 
        this.isCollapsed = true;
        return;
      }
      else {
        if(this.employeeId > 0) {
          const employeeRequest: EmployeeDTO = this.prepareEmployeeDTO();
          employeeRequest.id = this.employeeId;

          this.apiConnectionService.updateEmployee(employeeRequest).subscribe({
            next: (message) => {
              this.alertService.showAlert(message, 'success');
              this.resetForm();
              this.isCollapsed = true;
              this.refreshEmployeeList();
            },
            error: (error) => {
              const message = `Error al crear el empleado: ${error.message || error}`;
              this.alertService.showAlert(message, 'error');
              this.isCollapsed = false;
            }
          });
        }
        else {
          const employeeRequest: EmployeeDTO = this.prepareEmployeeDTO();
  
          this.apiConnectionService.createEmployee(employeeRequest).subscribe({
            next: () => {
              const message = 'Empleado creado';
              this.alertService.showAlert(message, 'success');
              this.resetForm();
              this.isCollapsed = true;
              this.refreshEmployeeList();
            },
            error: (error) => {
              const message = `Error al crear el empleado: ${error.message || error}`;
              this.alertService.showAlert(message, 'error');
              this.isCollapsed = true;
            }
          });
        } 
      } 
    }  else {
      this.alertService.showAlert('Por favor llene los campos requeridos.', 'error');
      this.isCollapsed = true;
    }
  }
  
  refreshEmployeeList() {
    this.reactiveSharedService.getEmployees();
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
      phoneNumber: this.phoneNumber,
      countryName: ''
    }

    return employeeRequest;
  }

  public resetForm(): void {
    this.employeeId = 0;
    this.selectCountryId = 0;
    this.name = '';
    this.lastName = '';
    this.birthDay = null!;
    this.phoneNumber = '';
    this.curp = '';
    this.ssn = '';
    this.employeeNumber = 0;
    this.activateSubmitButton = true;      
    this.employeeForm.resetForm();
  }
}
