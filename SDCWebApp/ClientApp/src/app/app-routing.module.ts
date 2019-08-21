import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';
import { TicketsSummaryComponent } from './tickets-summary/tickets-summary.component';


const routes: Routes = [
  { path: 'tickets-summary', component: TicketsSummaryComponent },
  { path: 'ticket-order', component: TicketOrderComponent },
  { path: 'ticket-tariff', component: TicketTariffComponent },
  { path: 'footer-login', component: FooterLoginComponent },
  { path: 'footer-logout', component: FooterLogoutComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', pathMatch: 'full', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
