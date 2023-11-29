import { Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { AuthService } from 'src/app/Services/auth.service';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

declare let M: any;

@Component({
  selector: 'app-list-user-view',
  templateUrl: './list-user-view.component.html',
  styleUrls: ['./list-user-view.component.css']
})
export class ListUserViewComponent implements OnInit {
  token: any;
  Users: any;
  IdSelected: number | undefined;
  nameSelected: string | undefined;
  instances: any;

  @ViewChild('modal') modal: ElementRef | undefined;

  constructor(private navBarComponent: NavBarComponent,
              private authService: AuthService) {}
  
  ngOnInit(): void {

    document.addEventListener('DOMContentLoaded', function() {
      const elems = document.querySelectorAll('.modal');
      M.Modal.init(elems);
    });

    this.authService.getUsers();
    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

    this.getUser();
  }

  getUser(){
    this.authService.getUsers().subscribe((response: any) => {
      this.Users = response.data;
    })
  }

  deleteUser(){
    this.authService.deleteUser(this.IdSelected!).subscribe(() => {
      this.getUser();
      M.toast({ html: 'Usuario Eliminado Correctamente', classes: 'green darken-1'});
    });
  }

  DataSelect(id: number, fullName: string){
    this.IdSelected = id;
    this.nameSelected = fullName;
  }

}
