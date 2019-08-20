import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { OrderGroupComponent } from './order-group/order-group.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';


const routes: Routes = [
  { path: 'ticket-order', component: TicketOrderComponent },
  { path: 'ticket-tariff', component: TicketTariffComponent },
  { path: 'order-group', component: OrderGroupComponent },
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
