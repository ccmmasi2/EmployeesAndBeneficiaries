import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatalogRoutingModule } from './catalog-routing.module';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { MaterialModule } from '@app/material/material.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    EmployeeListComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule,
    MaterialModule,
    FormsModule
  ]
})
export class CatalogModule { }
