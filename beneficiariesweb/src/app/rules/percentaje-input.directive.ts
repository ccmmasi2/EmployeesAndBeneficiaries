import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appPercentajeInput]'
})
export class PercentajeInputDirective {

  constructor(private el: ElementRef<HTMLInputElement>) { }

  @HostListener('input', ['$event']) onInput(event: Event): void {
    event.stopPropagation(); 
    const input = event.target as HTMLInputElement;
    let value = input.value;

    value = value.replace(/[^0-9]/g, '');
    const numValue = parseInt(value, 10);

    if (!value || (numValue >= 1 && numValue <= 100)) {
      input.value = value;
    } else {
      input.value = numValue > 100 ? '100' : '1';
    }
  }
}
