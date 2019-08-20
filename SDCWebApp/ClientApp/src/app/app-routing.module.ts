import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';


const routes: Routes = [
  { path: 'footer-login', component: FooterLoginComponent },
  { path: 'footer-logout', component: FooterLogoutComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', pathMatch: 'full', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
