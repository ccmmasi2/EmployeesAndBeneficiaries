import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { BeneficiaryFormComponent } from './beneficiary-form/beneficiary-form.component';

const routes: Routes = [
  {
    path: 'system',
    children: [
      {
        path: 'employees',
        component: EmployeeFormComponent,
      },
      {
        path: 'beneficiaries',
        component: BeneficiaryFormComponent,
      },
      {
        path: 'system/beneficiaries/:employeeId',
        component: BeneficiaryFormComponent,
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class CatalogRoutingModule { }
