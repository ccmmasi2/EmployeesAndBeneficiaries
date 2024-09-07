import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowMessageComponent } from './show-message/show-message.component';
import { FormsModule } from '@angular/forms';
import { ActionsDialogComponent } from './actions-dialog/actions-dialog.component';

@NgModule({
  declarations: [
    ShowMessageComponent,
    ActionsDialogComponent,
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    ShowMessageComponent,
  ]
})

export class SharedModule { }
