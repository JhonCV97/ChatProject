import { Component, Injectable, OnInit } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { AuthService } from '../../Services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { FormBuilder } from '@angular/forms';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

declare let M: any;

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit{

  isUserLoggedIn: boolean | undefined;
  UserLogin: any;
  UserUpdate: any;
  inputName: string | undefined;
  inputEmail: string | undefined;
  formForSubmit: any;
  roleId: number | undefined;

  constructor(private navBarComponent: NavBarComponent, 
              private authService: AuthService, 
              private formBuilder: FormBuilder
  ) {}
  
  async ngOnInit(): Promise<void> {
    if (localStorage.getItem("token")) {
      this.navBarComponent.refreshComponent();
    }

    this.formForSubmit = this.formBuilder.group({
      Name: '',
      Email: '',
      Password: '',
      RewritePassword: ''
    });

    if (localStorage.getItem("Login")) {
      
      this.UserLogin = localStorage.getItem('Login');

      this.UserLogin = JSON.parse(this.UserLogin);
    }

    this.authService.getUserById(this.UserLogin.id);

    await this.waitForCategory();

    this.UserUpdate = JSON.parse(this.UserUpdate);
    this.roleId! = this.UserUpdate.roleId;

    this.inputName = this.UserUpdate.fullName;
    this.inputEmail = this.UserUpdate.email;

  }

  waitForCategory(): Promise<void> {
    return new Promise<void>((resolve) => {
      const checkInterval = setInterval(() => {
        this.UserUpdate = localStorage.getItem('User');
        if (this.UserUpdate !== null) {
          clearInterval(checkInterval);
          resolve();
        }
      }, 100);
    });
  }

  validatePassword(){

    if (this.formForSubmit.controls["Password"].value == "" || this.formForSubmit.controls["RewritePassword"].value == "") {
      M.toast({ html: 'Los Campos de contraseña no pueden ser vacios', classes: 'red accent-3'});
    }
    else if (this.formForSubmit.controls["Password"].value != this.formForSubmit.controls["RewritePassword"].value) {
      M.toast({ html: 'Las contraseñas son diferentes', classes: 'red accent-3'});
    }

  }

  UpdateUser(){
    if(this.formForSubmit.valid) {
      if (this.formForSubmit.controls["Password"].value == this.formForSubmit.controls["RewritePassword"].value) {
        this.authService.UpdateUser(this.formForSubmit.controls["Email"].value, this.formForSubmit.controls["Password"].value, this.UserLogin.id, this.formForSubmit.controls["Name"].value, this.roleId!, '', '');
      }
    }
  }

}
