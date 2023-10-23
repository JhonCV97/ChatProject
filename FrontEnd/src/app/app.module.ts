import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './Components/signin/signin.component';
import { CategoriesIAComponent } from './Components/categories-ia/categories-ia.component';
import { RegisterComponent } from './Components/register/register.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { FooterComponent } from './Components/footer/footer.component';
import { RecoverPasswordComponent } from './Components/recover-password/recover-password.component';
import { ConfigurationComponent } from './Components/configuration/configuration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './Services/auth.service';
import { CategoriesAIService } from './Services/categories-ai.service';
import { ChatComponent, DataSharingService } from './Components/chat/chat.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    CategoriesIAComponent,
    RegisterComponent,
    NavBarComponent,
    FooterComponent,
    RecoverPasswordComponent,
    ConfigurationComponent,
    ChatComponent,
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
    CategoriesAIService,
    NavBarComponent, 
    DataSharingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
