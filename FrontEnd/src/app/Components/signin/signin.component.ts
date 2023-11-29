import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';


declare let M: any;

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})

export class SigninComponent implements OnInit{

  formForSubmit: any;
  formSubmit: any;
  inputEmail: string | undefined;

  constructor(public authService: AuthService, private formBuilder: FormBuilder, private router: Router) {}

  async ngOnInit(): Promise<void> {

    document.addEventListener('DOMContentLoaded', function() {
      const elems = document.querySelectorAll('.modal');
      const instances = M.Modal.init(elems);
    });

    this.formForSubmit = this.formBuilder.group({
      Email: '',
      Password: '',
    });

    this.formSubmit = this.formBuilder.group({
      Email: '',
    });

    await this.waitForEmail();
  }

  waitForEmail(): Promise<void> {
    return new Promise<void>((resolve) => {
      const checkInterval = setInterval(() => {
        if (localStorage.getItem('SendEmail') !== null) {
          clearInterval(checkInterval);
          const modalInstance = M.Modal.getInstance(document.getElementById('modal5'));
          modalInstance.close();
          M.toast({ html: 'Correo Enviado Correctamente', classes: 'green darken-1'});
          resolve();
          localStorage.removeItem("SendEmail");
        }
      }, 100);
    });
  }

  HiddenInfo(){
    this.inputEmail = "";
  }

  login() {
    if(this.formForSubmit.valid) {
      this.authService.Login(this.formForSubmit.controls["Email"].value, this.formForSubmit.controls["Password"].value)
      .subscribe(
        response => {

          const responseData = JSON.parse(JSON.stringify(response));

          localStorage.setItem("token", responseData.token);
          localStorage.setItem('Login', JSON.stringify(responseData.user));

          if (responseData.user.roleId == 1) {
            this.router.navigate(['/adminview']);
          }else{
            this.router.navigate(['/chat']);
          }
        },
        error => {
          console.error('Error:', error);
          M.toast({ html: 'Usuario o Contraseña Incorrectos', classes: 'red accent-3'});
        }
      );      
    }
  }

  SendEmail(){
    if(this.formSubmit.valid) {
      this.authService.SendEmail(this.formSubmit.controls["Email"].value);      
    }
  }

}
