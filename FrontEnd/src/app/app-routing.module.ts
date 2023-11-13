import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninComponent } from './Components/signin/signin.component';
import { RegisterComponent } from './Components/register/register.component';
import { RecoverPasswordComponent } from './Components/recover-password/recover-password.component';
import { ConfigurationComponent } from './Components/configuration/configuration.component';
import { ChatComponent } from './Components/chat/chat.component';
import { PaypalComponent } from './Components/paypal/paypal.component';
import { AdminViewComponent } from './Components/admin-view/admin-view.component';
import { ExcelLoadComponent } from './Components/excel-load/excel-load.component';
import { ListUserViewComponent } from './Components/list-user-view/list-user-view.component';

const routes: Routes = [ 
  {path: '', component: SigninComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'configuration', component: ConfigurationComponent},
  {path: 'recoverPassword', component: RecoverPasswordComponent},
  {path: 'chat', component: ChatComponent},
  {path: 'paypal', component: PaypalComponent},
  {path: 'adminview', component: AdminViewComponent},
  {path: 'loadexcel', component: ExcelLoadComponent},
  {path: 'listuser', component: ListUserViewComponent},
  {path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
