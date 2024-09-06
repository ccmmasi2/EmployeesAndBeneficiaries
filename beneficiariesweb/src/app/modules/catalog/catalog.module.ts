import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatalogRoutingModule } from './catalog-routing.module';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { MaterialModule } from '@app/material/material.module';
import { FormsModule } from '@angular/forms';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { BeneficiaryListComponent } from './beneficiary-list/beneficiary-list.component';
import { BeneficiaryFormComponent } from './beneficiary-form/beneficiary-form.component';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeFormComponent,
    BeneficiaryListComponent,
    BeneficiaryFormComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule,
    MaterialModule,
    FormsModule
  ]
})
export class CatalogModule { }
