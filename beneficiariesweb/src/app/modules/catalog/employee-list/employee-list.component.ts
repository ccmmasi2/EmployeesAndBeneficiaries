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
  ];
  dataSource: MatTableDataSource<EmployeeDTO>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  length!: number;
  pageIndex: number = 0; 
  pageSizeLength: number = 10;
  sorting: string = '';

  constructor(
    private alertService: AlertService,
    public employeeSharedService: EmployeeSharedService,
    public _MatPaginatorIntl: MatPaginatorIntl,
  ) {
    this.dataSource = new MatTableDataSource<EmployeeDTO>();
   }

  ngOnInit(): void {
    this.refreshEmployeeList(this.pageIndex + 1, this.pageSizeLength, this.sorting);
    this.getList();
    this.initializePagination(this.pageIndex, this.pageSizeLength, this.length);
  }
 
  getList(): void { 
    this.employeeSharedService.employees$.subscribe({
      next: data => {
        this.dataSource.data = data.data;
        this.length = data.totalRecords;
        this.dataSource.paginator = this.paginator;
      },
      error: error => {
        this.alertService.showAlert(`Error loading employees: ${error}`, 'error');
      }
    }); 
  }

  refreshEmployeeList(page: number, sizePage: number, sorting: string) {
    this.employeeSharedService.getEmployees(page, sizePage, sorting);
  }

  pageChanged(event: PageEvent) { 
    this.pageSizeLength = event.pageSize;
    this.refreshEmployeeList(this.pageIndex + 1, this.pageSizeLength, this.sorting);
    this.getList();
    this.initializePagination(event.pageIndex, this.pageSizeLength, this.length);
  }
  
  onSortChange(event: Sort): void {
    if (event.active && event.direction) {
      this.sorting = `${event.active} ${event.direction}`;
      this.refreshEmployeeList(this.pageIndex + 1, this.pageSizeLength, this.sorting);
      this.getList();
    }
  }

  initializePagination(pageObj: number, pageSizeObj: number, lengthObj: number){
    this._MatPaginatorIntl.itemsPerPageLabel = 'Show';
    this._MatPaginatorIntl.nextPageLabel = 'Next page';
    this._MatPaginatorIntl.previousPageLabel = 'Last page';

    this._MatPaginatorIntl.getRangeLabel = (
      page: number = pageObj,
      pageSize: number = pageSizeObj,
      length: number = lengthObj
    ) => {
      if (length === 0 || pageSize === 0) {
        return '';
      } 

      const startIndex = page * pageSize;
      const endIndex = Math.min(startIndex + pageSize, length);

      let countPages = Math.ceil(length / pageSizeObj);
      const spaces = String.fromCharCode(160).repeat(30); 

      return `${startIndex + 1} - ${endIndex} of ${length} entries ${spaces} Pag ${pageObj + 1} / ${countPages}`;
    }; 
  }

  columnMapping: { [key: string]: string } = {
    'Id': 'id',
    'Name': 'name',
  };
}
