import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
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
    'actions'
  ];
  dataSource: MatTableDataSource<EmployeeDTO>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  length!: number;
  pageIndex: number = 0; 
  pageSizeLength: number = 10;
  sorting: string = '';
  pageSizeOptions = [10, 25, 50];

  constructor(
    private alertService: AlertService,
    public employeeSharedService: EmployeeSharedService,
    public _MatPaginatorIntl: MatPaginatorIntl,
  ) {
    this.dataSource = new MatTableDataSource<EmployeeDTO>();
   }

  ngOnInit(): void {
    this.refreshEmployeeList(this.pageIndex, this.pageSizeLength, this.sorting);
    this.subscribeToData();
  }
 
  subscribeToData(): void { 
    this.employeeSharedService.employees$.subscribe({
      next: data => {
        this.dataSource.data = data.data;
        this.length = data.totalRecords;
      },
      error: error => {
        this.alertService.showAlert(`Error loading employees: ${error}`, 'error');
      }
    }); 
  }

  refreshEmployeeList(page: number, sizePage: number, sorting: string) {
    this.employeeSharedService.getEmployees(page + 1, sizePage, sorting);
  }  

  pageChanged(event: PageEvent) { 
    this.pageIndex = event.pageIndex;
    this.pageSizeLength = event.pageSize; 
    this.refreshEmployeeList(this.pageIndex, this.pageSizeLength, this.sorting);
  }
  
  onSortChange(event: Sort): void {
    if (event.active && event.direction) {
      this.sorting = `${event.active} ${event.direction}`;
      this.refreshEmployeeList(this.pageIndex, this.pageSizeLength, this.sorting);
    }
  } 
}
