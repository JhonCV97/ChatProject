import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';

declare let M: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

  constructor(private authService: AuthService, 
              private formBuilder: FormBuilder
  ) {}
  
  formForSubmit: any;

  ngOnInit(): void {

    this.formForSubmit = this.formBuilder.group({
      Name: '',
      Email: '',
      Password: '',
      RewritePassword: '' 
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


  AddUser(){
    if(this.formForSubmit.valid) {
      if (this.formForSubmit.controls["Password"].value == this.formForSubmit.controls["RewritePassword"].value) {
        this.authService.AddUser(this.formForSubmit.controls["Email"].value, this.formForSubmit.controls["Password"].value, this.formForSubmit.controls["Name"].value);
      }
    }
  }






}
