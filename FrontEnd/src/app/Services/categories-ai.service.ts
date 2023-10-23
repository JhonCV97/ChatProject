import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CategoriesAIService {

  url: string = environment.url_BackEnd;
  headers = new HttpHeaders({ 'Content-Type': 'application/json-patch+json' });
  
  constructor(private router: Router, private _httpClient: HttpClient) { }

  AddCategory(title: string, description: string, image: File) {

    const headers = new HttpHeaders({ 'Content-Type': 'multipart/form-data; boundary=<calculated when request is sent>' });

    const formData = new FormData();

    formData.append('Image', image);
    formData.append('CategoriesAIPostDto.Name', title);
    formData.append('CategoriesAIPostDto.Description', description);
    formData.append('CategoriesAIPostDto.Status', 'true');

    return this._httpClient.post(`${this.url}/api/CategoriesAI`, formData)
      .subscribe(
        (response: any) => {
          if (response.result) {
            this.getCategories();
            window.location.reload();
          }
        },
        error => {
          console.error('Error:', error);
        }
      );
  }

  getCategories(){
    return this._httpClient.get(`${this.url}/api/CategoriesAI`)
    .subscribe(
      (response: any) => {
        
        const responseData = JSON.stringify(response.data);
        localStorage.setItem("CategoriesList", responseData);
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  deleteCategories(Id: string){
    return this._httpClient.delete(`${this.url}/api/CategoriesAI/${Id}`)
    .subscribe(
      (response: any) => {
        if (response.result) {
          this.getCategories();
          window.location.reload();
        }
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  getCategoriesById(Id: string){
    localStorage.removeItem("Category");
    return this._httpClient.get(`${this.url}/api/CategoriesAI/${Id}`)
    .subscribe(
      (response: any) => {
        const responseData = JSON.stringify(response.data);
        localStorage.setItem("Category", responseData);
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  UpdateCategory(title: string, description: string, image: File, Id: string) {

    const formData = new FormData();

    formData.append('Image', image);
    formData.append('CategoriesAIDto.Id', Id);
    formData.append('CategoriesAIDto.Name', title);
    formData.append('CategoriesAIDto.Description', description);
    formData.append('CategoriesAIDto.Status', 'true');

    return this._httpClient.put(`${this.url}/api/CategoriesAI`, formData)
      .subscribe(
        (response: any) => {
          console.log(response);
          
          if (response.result) {
            this.getCategories();
            window.location.reload();
          }
        },
        error => {
          console.error('Error:', error);
        }
      );
  }
}
