import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from '@app/models/employee.model';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';
import { EmployeeSharedService } from '@app/services/employee-shared.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
})

export class EmployeeListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'employeeNumber',
    'name',
    'lastName',
    'phoneNumber',
  ];

  dataSource: EmployeeDTO[] = [];

  constructor(
    private alertService: AlertService,
    public employeeSharedService: EmployeeSharedService
  ) { }

  ngOnInit(): void {
    this.getList();
  }
 
  getList(): void { 
    this.employeeSharedService.employees$.subscribe(
      employees => {
        this.dataSource = employees;
      },
      error => {
        this.alertService.showAlert(`Error loading employees: ${error}`, 'error');
      }
    );

    this.refreshEmployeeList();
  }

  refreshEmployeeList() {
    this.employeeSharedService.getEmployees();
  }
}
