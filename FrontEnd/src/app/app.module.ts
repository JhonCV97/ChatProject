import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './Components/signin/signin.component';
import { RegisterComponent } from './Components/register/register.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { FooterComponent } from './Components/footer/footer.component';
import { RecoverPasswordComponent } from './Components/recover-password/recover-password.component';
import { ConfigurationComponent } from './Components/configuration/configuration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './Services/auth.service';
import { ChatComponent, DataSharingService } from './Components/chat/chat.component';
import { PaypalComponent } from './Components/paypal/paypal.component';
import { ExcelLoadComponent } from './Components/excel-load/excel-load.component';
import { ListUserViewComponent } from './Components/list-user-view/list-user-view.component';
import { AdminViewComponent } from './Components/admin-view/admin-view.component';
import { ReportComponent } from './Components/report/report.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    RegisterComponent,
    NavBarComponent,
    FooterComponent,
    RecoverPasswordComponent,
    ConfigurationComponent,
    ChatComponent,
    PaypalComponent,
    ExcelLoadComponent,
    ListUserViewComponent,
    AdminViewComponent,
    ReportComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    AuthService, 
    NavBarComponent, 
    DataSharingService,
    ChatComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
