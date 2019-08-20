import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { OrderGroupComponent } from './order-group/order-group.component';
import { TicketOverviewComponent } from './ticket-overview/ticket-overview.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FooterLoginComponent,
    FooterLogoutComponent,
    OrderGroupComponent,
    TicketOverviewComponent,
    TicketTariffComponent,
    TicketOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
