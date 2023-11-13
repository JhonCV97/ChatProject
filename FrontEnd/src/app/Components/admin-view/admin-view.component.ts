import { Component, Injectable, OnInit } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isUserRegister: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isUserAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})
export class AdminViewComponent implements OnInit {

  Login: any;
  token: any;

  constructor(private navBarComponent: NavBarComponent) {}

  ngOnInit(): void {

    this.Login! = localStorage.getItem("Login");

    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

  }

}
