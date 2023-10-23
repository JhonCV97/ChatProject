import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  url: string = environment.url_BackEnd;
  headers = new HttpHeaders({ 'Content-Type': 'application/json-patch+json' });

  constructor(
    private router: Router,
    private _httpClient: HttpClient
  ) {}
  
  Login(email: string, password: string) {

    const headers = this.headers;

    const request = {
      "login": email,
      "password": password
    };

    return this._httpClient.post(`${this.url}/api/auth-token`, request, { headers })
      .subscribe(
        response => {

          const responseData = JSON.parse(JSON.stringify(response));

          localStorage.setItem("token", responseData.token);
          localStorage.setItem('Login', JSON.stringify(responseData.user));
          this.router.navigate(['/chat']);
          
        },
        error => {
          console.error('Error:', error);
        }
      );
  }

  getUserById(Id: string){
    localStorage.removeItem("User");
    return this._httpClient.get(`${this.url}/api/User/${Id}`)
    .subscribe(
      (response: any) => {
        const responseData = JSON.stringify(response.data);
        localStorage.setItem("User", responseData);
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  UpdateUser(email: string, password: string, Id: string, name: string, lastname: string, role: number) {

    const headers = this.headers;

    const request = {
      "userDto": {
        "id": Id,
        "name": name,
        "lastname": lastname,
        "email": email,
        "login": email,
        "password": password,
        "roleId": role,
      }
    };

    console.log(request);
    
    
    return this._httpClient.put(`${this.url}/api/User`, request)
      .subscribe(
        (response: any) => {
          if (response.result) {
            this.getUserById(Id);
            if (Id == '0' && role == 0) {
              this.router.navigate(['/']);
            }else{
              window.location.reload();
            }
          }
        },
        error => {
          console.error('Error:', error);
        }
      );
  }


  AddUser(email: string, password: string, name: string, lastname: string) {

    const headers = this.headers;

    const request = {
      "userPostDto": {
        "name": name,
        "lastname": lastname,
        "email": email,
        "login": email,
        "password": password,
        "roleId": 2,
      }
    };

    return this._httpClient.post(`${this.url}/api/User`, request)
      .subscribe(
        (response: any) => {
          if (response.result) {
            this.router.navigate(['/']);
          }
        },
        error => {
          console.error('Error:', error);
        }
      );
  }

  SendEmail(email: string) {

    const headers = this.headers;

    const request = {
      "email": email
    };

    return this._httpClient.post(`${this.url}/api/User/RecoverPassword`, request)
      .subscribe(
        (response: any) => {
          const responseData = JSON.stringify(response.result);
          localStorage.setItem("SendEmail", responseData);
          if (response.result) {
            this.router.navigate(['/']);
          }
        },
        error => {
          console.error('Error:', error);
        }
      );
  }

}


