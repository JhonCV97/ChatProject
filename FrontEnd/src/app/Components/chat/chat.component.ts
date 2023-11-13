import { AfterViewChecked, Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { HistoryService } from 'src/app/Services/history.service';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isUserRegister: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isUserAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isPremium: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {

  Login: any;
  token: any;
  isPremium: boolean | undefined;
  DiplayChat: boolean = false;
  UserLogin: any;
  Histories: any;
  Chats: any;
  NewMessageChat: any;
  MessageInput: string = '';
  ScrollDown: boolean = false;
  ParentId: number | null = null;

  constructor(private navBarComponent: NavBarComponent,
              private historyService: HistoryService,
              private dataSharingService: DataSharingService) {
                this.dataSharingService.isPremium.subscribe(value => {
                    this.isPremium = value;
                });
              }
  
  ngAfterViewChecked(): void {
    if (this.ScrollDown) {
      let elemento = document.querySelector('#divMessage');    
      if (elemento) {
        elemento.scrollTop = elemento.scrollHeight;
      }
      this.ScrollDown = false;
    }
    
  }

  ngOnInit(): void {
    
    this.UserLogin = localStorage.getItem("Login");
    this.UserLogin = JSON.parse(this.UserLogin);

    this.historyService.GetChatsBySession(0, this.UserLogin.id).subscribe((response: any) => {      
      this.Histories = response.data
    });

    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

  }

  refreshComponent() {
    this.dataSharingService.isPremium.next(true);
  }
  
  getMessageByChat(ChatId: number){
    this.DiplayChat = false;
    this.ParentId = ChatId;
    
    this.historyService.GetChatsBySession(ChatId, this.UserLogin.id).subscribe((response: any) => {
      this.Chats = [];
      this.Chats.push(this.Histories.find((x: any) => x.history.id == ChatId));      
      this.Chats = [...this.Chats, ...response.data];
      this.ScrollDown = true;
    });
  }

  newChat(){
    this.ParentId = null;
    this.DiplayChat = false;
    this.Chats = [];
  }

  newMessage(){
    this.DiplayChat = true;
    this.NewMessageChat = this.MessageInput;
    this.MessageInput = '';
    this.ScrollDown = true;
    let currentDate = this.getDate();
    
    this.historyService.AddHistory(this.UserLogin.id, currentDate, this.NewMessageChat, this.ParentId).subscribe((response: any) => {
      console.log(response);
      
    });
  }


  getDate(){
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth() + 1;
    const day = currentDate.getDate();
    const hours = currentDate.getHours();
    const minutes = currentDate.getMinutes();
    const seconds = currentDate.getSeconds();
    
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
  }

}
