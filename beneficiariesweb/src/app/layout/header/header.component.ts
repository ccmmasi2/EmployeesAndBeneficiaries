import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})

export class HeaderComponent {

  constructor(private router: Router) {}

  onOptionChange(event: any) {
    const selectedValue = event.target.value;

    if (selectedValue === '1') {
      this.router.navigate(['']);
    } else if (selectedValue === '2') {
      this.router.navigate(['/system/catalog']);
    } else if (selectedValue === '3') {
      this.router.navigate(['/system/shopping-cart']);
    }
  }

  onHomeButtonClick() {
    this.router.navigate(['']);

    const selectElement = document.getElementById('selectOption') as HTMLSelectElement;
    if (selectElement) {
      selectElement.selectedIndex = 0;
    }
  }
}
