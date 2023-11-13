import { Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ChatComponent } from '../chat/chat.component';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

declare var paypal : any;

@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.css']
})
export class PaypalComponent implements OnInit{
  
  token: any;
  UserLogin: any;

  constructor(private navBarComponent: NavBarComponent,
              private authService: AuthService,
              private router: Router,
              private chatComponent: ChatComponent) {}

  @ViewChild('paypal', {static:true}) paypalElement : ElementRef | undefined;

  product = {
    description: 'Suscripcion Chat Ucaldas',
    price: 1.00
  }

  ngOnInit(): void {

    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

    paypal
    .Buttons({ 
      createOrder: (data: any, actions: any) => {
        return actions.order.create({
          purchase_units:[{
            description: this.product.description,
            amount: {
              currency_code: 'USD',
              value: this.product.price
            }
          }]
        })
      },
      onApprove: async(data: any, actions: any) =>{
        const order = await actions.order.capture();

        if (order.status == "COMPLETED") {
          this.PayPremiumUser();
          this.chatComponent.refreshComponent();
          this.router.navigate(['/chat']);
        }

      },
      OnError: (err: any) => {
        console.log(err);
        
      }
    })
    .render(this.paypalElement?.nativeElement);
  }

  PayPremiumUser(){
    this.UserLogin = localStorage.getItem("Login");
    this.UserLogin = JSON.parse(this.UserLogin);
    
    this.authService.PayUserPremium(this.UserLogin.id);
  }
  
}
