import { Component, OnInit } from '@angular/core';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';

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

  dataSource: any;

  constructor(
    public apiConnectionService: ApiConnectionService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.getList();
  }
 
  getList(): void {
    this.apiConnectionService.getEmployees().subscribe((data) => {
      if (data) {
        this.dataSource = data;
      }
    },
    (error) => {
      const message = `Error getting data from the API "${error}"`
      this.alertService.showAlert(message, 'error'); 
    });
  }
}
