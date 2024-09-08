import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BeneficiaryDTO } from '@app/models/beneficiary.model';
import { ActionsDialogComponent } from '@app/modules/shared/actions-dialog/actions-dialog.component';
import { AlertService } from '@app/services/alert-service.service';
import { ReactiveSharedService } from '@app/services/reactive-shared.service';

@Component({
  selector: 'app-beneficiary-list',
  templateUrl: './beneficiary-list.component.html',
})

export class BeneficiaryListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'employeeNumber',
    'employeeName',
    'name',
    'lastName',
    'birthDay',
    'curp',
    'ssn',
    'phoneNumber',
    'countryName',
    'participationPercentaje',
    'actions'
  ];
  dataSource: MatTableDataSource<BeneficiaryDTO>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  length!: number;
  pageIndex: number = 0; 
  pageSizeLength: number = 10;
  sorting: string = '';
  pageSizeOptions = [10, 25, 50];

  constructor(
    private alertService: AlertService,
    public reactiveSharedService: ReactiveSharedService,
    public _MatPaginatorIntl: MatPaginatorIntl,
    private dialog: MatDialog,
  ) {
    this.dataSource = new MatTableDataSource<BeneficiaryDTO>();
   }

  ngOnInit(): void {
    this.refreshBeneficiariesList(this.pageIndex, this.pageSizeLength, this.sorting);
    this.subscribeToData();
  }
 
  subscribeToData(): void { 
    this.reactiveSharedService.beneficiaries$.subscribe({
      next: data => {
        this.dataSource.data = data.data;
        this.length = data.totalRecords;
      },
      error: error => {
        this.alertService.showAlert(`Error loading beneficiaries: ${error}`, 'error');
      }
    }); 
  }

  refreshBeneficiariesList(page: number, sizePage: number, sorting: string) {
    this.reactiveSharedService.getBeneficiaries(page + 1, sizePage, sorting);
  }  

  pageChanged(event: PageEvent) { 
    this.pageIndex = event.pageIndex;
    this.pageSizeLength = event.pageSize; 
    this.refreshBeneficiariesList(this.pageIndex, this.pageSizeLength, this.sorting);
  }
  
  onSortChange(event: Sort): void {
    if (event.active && event.direction) {
      this.sorting = `${event.active} ${event.direction}`;
      this.refreshBeneficiariesList(this.pageIndex, this.pageSizeLength, this.sorting);
    }
  } 

  openActionsDialog(event: MouseEvent, row: any) {
    const beneficiaryObject: BeneficiaryDTO = row as BeneficiaryDTO;
    const offsetX = 240;
    const offsety = 35;
    this.dialog.open(ActionsDialogComponent, {
      data: { employeeId: beneficiaryObject.employeeId },
      position: {
        top: event.clientY - offsety + 'px',
        left: event.clientX - offsetX + 'px',
      },
    });
  }
}
