import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EventService } from '@app/services/event.service';

@Component({
  selector: 'app-actions-dialog',
  templateUrl: './actions-dialog.component.html',
})

export class ActionsDialogComponent  implements OnInit { 
  employeeId!: number;

  constructor(
    public dialogRef: MatDialogRef<ActionsDialogComponent>,
    private eventService: EventService,
    @Inject(MAT_DIALOG_DATA) public data: any
    ) { }

  ngOnInit(): void {
    this.employeeId = this.data.employeeId;
  }
 
  invokeWatchClickEvent() {
    this.eventService.emitWatchButtonClick(this.employeeId);
    this.dialogRef.close(); 
  }
 
  invokeEditClickEvent() {
    this.eventService.emitEditButtonClick(this.employeeId);
    this.dialogRef.close(); 
  }
 
  invokeDeleteClickEvent() {
    this.eventService.emitDeleteButtonClick(this.employeeId);
    this.dialogRef.close(); 
  }
 
  invokeWatchBeneficiariesClickEvent() {
    this.eventService.emitWatchBeneficiariesButtonClick(this.employeeId);
    this.dialogRef.close(); 
  }
}
