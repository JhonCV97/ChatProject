import { Component, Injectable, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { AuthService } from 'src/app/Services/auth.service';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

@Component({
  selector: 'app-list-user-view',
  templateUrl: './list-user-view.component.html',
  styleUrls: ['./list-user-view.component.css']
})
export class ListUserViewComponent implements OnInit {
  token: any;
  Users: any;

  constructor(private navBarComponent: NavBarComponent,
              private authService: AuthService) {}
  
  ngOnInit(): void {

    this.authService.getUsers();
    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

    this.Users = localStorage.getItem('UsersList');
    this.Users = JSON.parse(this.Users);

    console.log(this.Users);
    

  }
}
