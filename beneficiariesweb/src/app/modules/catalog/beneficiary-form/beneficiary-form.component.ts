import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BeneficiaryDTO } from '@app/models/beneficiary.model';
import { CountryDTO } from '@app/models/country.model';
import { EmployeeDTO } from '@app/models/employee.model';
import { ConfirmDialogComponent } from '@app/modules/shared/confirm-dialog/confirm-dialog.component';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';
import { EventService } from '@app/services/event.service';
import { ReactiveSharedService } from '@app/services/reactive-shared.service';

@Component({
  selector: 'app-beneficiary-form',
  templateUrl: './beneficiary-form.component.html',
})

export class BeneficiaryFormComponent implements OnInit {
  isCollapsed: boolean = true;
 
  @ViewChild('beneficiaryForm') beneficiaryForm!: NgForm; 
  cartItems: BeneficiaryDTO[] = []; 

  searchEmployeeTerm: string = '';
  beneficiaryId: number = 0;
  selectEmployeeId: number = 0;
  selectCountryId: number = 0;
  employeeResults: EmployeeDTO[] = []; 
  countryIdOptions: CountryDTO[] = [];
  name: string = '';
  lastName: string = '';
  birthDay: string = '';
  phoneNumber: string = '';
  curp: string = '';
  ssn: string = '';
  participationPercentaje: number = 0;
  activateSubmitButton: boolean = true;
  
  constructor(
    public apiConnectionService: ApiConnectionService,
    private alertService: AlertService,
    public reactiveSharedService: ReactiveSharedService,
    private eventService: EventService,
    private dialog: MatDialog
  ) {
    this.eventService.watchButtonClick.subscribe((beneficiaryId: number) => {
      this.loadBeneficiary(beneficiaryId);
      this.beneficiaryId = 0;
      this.activateSubmitButton = false;
    });
    this.eventService.editButtonClick.subscribe((beneficiaryId: number) => {
      this.loadBeneficiary(beneficiaryId);
      this.beneficiaryId = beneficiaryId;
      this.activateSubmitButton = true;      
    });
    this.eventService.deleteButtonClick.subscribe((beneficiaryId: number) => {
      this.deleteBeneficiary(beneficiaryId);
    });
  }

  loadBeneficiary(beneficiaryId: number){
    this.apiConnectionService.getBeneficiaryXId(beneficiaryId)
    .subscribe((beneficiary) => { 
      beneficiary.birthDay = new Date(beneficiary.birthDay).toISOString().split('T')[0];  
      this.beneficiaryForm.reset(beneficiary);
      this.selectCountryId = beneficiary.countryId;
      this.isCollapsed = false; 
    });
  }  

  deleteBeneficiary(beneficiaryId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px'
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiConnectionService.deleteBeneficiary(beneficiaryId).subscribe(
          (response) => { 
            this.alertService.showAlert(response, 'success');
            this.refreshBeneficiaryList();
          },
          (error) => {
            this.alertService.showAlert(`Error eliminando el beneficiario: ${error.message || error}`, 'error');
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

    const page = 1;  
    const sizePage = 1000;  
    const sorting = ''; 
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
    if (this.beneficiaryForm.valid) {
      if (!this.validateAge(this.birthDay)) {
        const message = `El beneficiario debe tener al menos 18 años`
        this.alertService.showAlert(message, 'error'); 
        this.isCollapsed = true;
        return;
      }
      else {
        console.log(this.beneficiaryId);
        if(this.beneficiaryId > 0) {
          const beneficiaryRequest: BeneficiaryDTO = this.prepareBeneficiaryDTO();
          beneficiaryRequest.id = this.beneficiaryId;

          this.apiConnectionService.updateBeneficiary(beneficiaryRequest).subscribe({
            next: (message) => {
              this.alertService.showAlert(message, 'success');
              this.resetForm();
              this.isCollapsed = true;
              this.refreshBeneficiaryList();
            },
            error: (error) => {
              const message = `Error al crear el beneficiario: ${error.message || error}`;
              this.alertService.showAlert(message, 'error');
              this.isCollapsed = false;
            }
          });
        }
        else {
          const beneficiaryRequest: BeneficiaryDTO = this.prepareBeneficiaryDTO();
  
          this.apiConnectionService.createBeneficiary(beneficiaryRequest).subscribe({
            next: () => {
              const message = 'Beneficiario creado';
              this.alertService.showAlert(message, 'success');
              this.resetForm();
              this.isCollapsed = true;
              this.refreshBeneficiaryList();
            },
            error: (error) => {
              const message = `Error al crear el beneficiario: ${error.message || error}`;
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
  
  refreshBeneficiaryList() {
    this.reactiveSharedService.getBeneficiaries();
  } 
  
  private prepareBeneficiaryDTO(): BeneficiaryDTO {
    const employeeRequest: BeneficiaryDTO = {
      id: 0,
      employeeId: this.selectEmployeeId,
      countryId: this.selectCountryId, 
      participationPercentaje: this.participationPercentaje,
      name: this.name,
      lastName: this.lastName, 
      birthDay: this.birthDay,
      curp: this.curp,
      ssn: this.ssn,
      phoneNumber: this.phoneNumber,
      countryName: '', 
      employeeName: '',
      employeeNumber: 0
    }

    return employeeRequest;
  }

  public resetForm(): void {
    this.selectEmployeeId = 0;
    this.selectCountryId = 0;
    this.name = '';
    this.lastName = '';
    this.birthDay = null!;
    this.phoneNumber = '';
    this.curp = '';
    this.ssn = '';
    this.participationPercentaje = 0;
    this.beneficiaryForm.resetForm();
  }

  onSearchChange(): void {
    if (this.searchEmployeeTerm.length > 1) {
      this.apiConnectionService.getEmployeesXFilter(this.searchEmployeeTerm)
          .subscribe(results => {
              this.employeeResults = results;
          }, error => {
              console.error('Failed to load employees:', error);
              this.alertService.showAlert('Error cargando empleados: ' + error.message, 'error');
          });
    } else {
      this.employeeResults = [];  
    }
  }

  selectEmployee(employee: EmployeeDTO): void {
    this.selectEmployeeId = employee.id;
    this.searchEmployeeTerm = `${employee.name} (${employee.id})`;   
    this.employeeResults = [];   
  }

  clearResults(): void {
    setTimeout(() => {   
        this.employeeResults = [];
    }, 200);
  }
}
