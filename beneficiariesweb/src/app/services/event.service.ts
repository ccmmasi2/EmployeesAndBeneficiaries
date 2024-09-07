import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class EventService {
  watchButtonClick: EventEmitter<number> = new EventEmitter<number>();

  emitWatchButtonClick(employeeId: number) {
    this.watchButtonClick.emit(employeeId);
  }

  emitEditButtonClick(employeeId: number) {
    this.watchButtonClick.emit(employeeId);
  }

  emitDeleteButtonClick(employeeId: number) {
    this.watchButtonClick.emit(employeeId);
  }

  emitWatchBeneficiariesButtonClick(employeeId: number) {
    this.watchButtonClick.emit(employeeId);
  }
}
