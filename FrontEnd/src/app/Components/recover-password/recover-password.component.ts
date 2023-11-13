import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.css']
})
export class RecoverPasswordComponent {

  constructor(private authService: AuthService, 
            private formBuilder: FormBuilder
  ) {}

  formForSubmit: any;

  ngOnInit(): void {
    this.formForSubmit = this.formBuilder.group({
      Email: '',
      Password: '',
      RewritePassword: '' 
    });

  }

  UpdateUser(){
    if(this.formForSubmit.valid) {
      if (this.formForSubmit.controls["Password"].value == this.formForSubmit.controls["RewritePassword"].value) {
        this.authService.UpdateUser(this.formForSubmit.controls["Email"].value, this.formForSubmit.controls["Password"].value, '0', '', 0, '', '');
      }
    }
  }

}
