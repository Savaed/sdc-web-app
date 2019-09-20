import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {path: 'home', component: HomeComponent },
  { path: 'ticket-tariff', component: TicketTariffComponent },
  { path: 'footer-login', component: FooterLoginComponent },
  { path: 'footer-logout', component: FooterLogoutComponent },
  { path: '**', pathMatch: 'full', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
