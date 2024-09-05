import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowMessageComponent } from './show-message/show-message.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ShowMessageComponent,
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
