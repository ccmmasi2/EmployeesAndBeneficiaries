import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EmployeeDTO } from '@app/models/employee.model';
import { AlertService } from '@app/services/alert-service.service';
import { ApiConnectionService } from '@app/services/api-connection.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
})

export class EmployeeFormComponent implements OnInit {
  
  @ViewChild('orderForm') orderForm!: NgForm; 
  cartItems: EmployeeDTO[] = []; 

  // selectIdentificationTypeId: number = 0;
  // identificationTypeOptions: IdentificationTypeDTO[] = [];

  name: string = '';
  lastName: string = '';
  birthDay!: Date;
  phoneNumber: string = '';
  
  constructor(
    public apiConnectionService: ApiConnectionService,
    private alertService: AlertService,
  ) {
  }

  ngOnInit(): void {
    this.loadDataOptions(); 
  }

  loadDataOptions(): void {
    // this.apiConnectionService.getIdentificationTypes().subscribe((info) => {
    //   if(info){
    //     this.identificationTypeOptions = info;
    //   }
    //   else {
    //     const message = `Error loading identification types`
    //     this.alertService.showAlert(message, 'error'); 
    //   }
    // },
    // (error) => {
    //   const message = `Error loading identification types: "${error}"`
    //   this.alertService.showAlert(message, 'error'); 
    // })
  }  

  validateAge(birthDay: Date | string): boolean {
    if (typeof birthDay === 'string') {
        birthDay = new Date(birthDay);
    }
    
    if (!(birthDay instanceof Date) || isNaN(birthDay.getTime())) {
        return false;
    }

    const currentDate = new Date();
    const diff = currentDate.getTime() - birthDay.getTime();
    const age = Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
    return age >= 18;
  }

  submitForm(): void {
    if (this.orderForm.valid) {
      if (!this.validateAge(this.birthDay)) {
        const message = `The customer must be over than 18 years old`
        this.alertService.showAlert(message, 'error'); 
        return;
      }
      else {
        // const orderRequest: OrderRequest = {
        //   customer: {
        //     name: this.name,
        //     lastName: this.lastName, 
        //     identificationTypeId: this.selectIdentificationTypeId, 
        //     identification: this.identification,
        //     birthDay: this.birthDay,
        //     email: this.email, 
        //     phoneNumber: this.phoneNumber
        //   },
        //   Products: this.cartItems,
        //   shippingAddress: this.shippingAddress
        // }
  
        // this.apiConnectionService.createOrder(orderRequest).subscribe(
        //   (response) => {
        //     localStorage.removeItem('cartItems');

        //     const message = `Order created successfully`
        //     this.alertService.showAlert(message, 'success'); 

        //     this.dialog.closeAll();

        //     this.router.navigate(['/Order/catalog']);
        //   },
        //   (error) => {
        //     const message = `An error occurred while creating the order: "${error}"`
        //     this.alertService.showAlert(message, 'error'); 
        //   }
        // );
      } 
    } 
  }
}
