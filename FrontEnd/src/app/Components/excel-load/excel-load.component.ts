import { Component, Injectable, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

@Component({
  selector: 'app-excel-load',
  templateUrl: './excel-load.component.html',
  styleUrls: ['./excel-load.component.css']
})
export class ExcelLoadComponent implements OnInit {
  
  token: any;

  constructor(private navBarComponent: NavBarComponent) {}
  
  ngOnInit(): void {

    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

  }

}
