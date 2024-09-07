import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class EventService {
  watchButtonClick: EventEmitter<number> = new EventEmitter<number>();

  emitWatchButtonClick(processId: number) {
    this.watchButtonClick.emit(processId);
  }
}
