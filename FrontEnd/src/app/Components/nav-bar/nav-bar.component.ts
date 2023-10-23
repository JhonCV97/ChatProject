import { Component, OnInit, ChangeDetectorRef, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { DataSharingService } from '../chat/chat.component';

declare let M: any;

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  isUserLoggedIn: boolean | undefined;
  isUserRegister: boolean | undefined;

  constructor(private dataSharingService: DataSharingService) {
    this.dataSharingService.isUserLoggedIn.subscribe(value => {
      this.isUserLoggedIn = value;
    });
    this.dataSharingService.isUserRegister.subscribe(value => {
      this.isUserRegister = value;
    });
  }

  token: any;

  ngOnInit(): void {
    document.addEventListener('DOMContentLoaded', function () {
      var elems = document.querySelectorAll('.sidenav');
      var instances = M.Sidenav.init(elems);
    });

    this.token! = localStorage.getItem("token");
  }

  DeleteToken() {
    localStorage.removeItem("token");
    localStorage.removeItem("Login");
  }

  refreshComponent() {
    this.dataSharingService.isUserLoggedIn.next(true);
    if(localStorage.getItem("Login")){
      this.dataSharingService.isUserRegister.next(true);
    }
  }
}
