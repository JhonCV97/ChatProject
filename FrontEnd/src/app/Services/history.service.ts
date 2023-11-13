import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  url: string = environment.url_BackEnd;
  constructor(private router: Router,
              private _httpClient: HttpClient) { }

  GetChatsBySession(ChatId: number, UserId: number){

    let token = localStorage.getItem("token");
    const headers = new HttpHeaders({ 'Authorization': 'Bearer '+token });

    let urlQuery = `${this.url}/api/History?ChatId=${ChatId}&UserId=${UserId}`;

    if (ChatId == 0) {
        urlQuery = `${this.url}/api/History?UserId=${UserId}`;
    }

    return this._httpClient.get(urlQuery, {headers});
  }

  AddHistory(UserId: number, QueryDate: string, Question: string, parentHistoryId: number | null){
    
    let token = localStorage.getItem("token");
    const headers = new HttpHeaders({ 'Authorization': 'Bearer '+token });

    const request = {
      "userId": UserId,
      "historyDtoPost": {
        "queryDate": QueryDate,
        "question": Question,
        "answer": "",
        "parentHistoryId": parentHistoryId
      }
    };

    return this._httpClient.post(`${this.url}/api/History`, request, {headers})
      // .subscribe(
      //   (response: any) => {
      //     if (response.result) {
      //       this.router.navigate(['/chat']);
      //     }
      //   },
      //   error => {
      //     console.error('Error:', error);
      //   }
      // );
  }

  DeleteHistory(){

  }
  
}
