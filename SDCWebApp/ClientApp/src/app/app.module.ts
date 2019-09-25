import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterLoginComponent } from './shared/footer-login/footer-login.component';
import { FooterLogoutComponent } from './shared/footer-logout/footer-logout.component';
import { TicketTariffComponent } from './shared/ticket-tariff/ticket-tariff.component';
import { HomeComponent } from './shared/home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { ArticlesComponent } from './shared/articles/articles.component';
import { LoginComponent } from './shared/login/login.component';
import { VisitSdcComponent } from './shared/visit-sdc/visit-sdc.component';
import { PartnersComponent } from './shared/partners/partners.component';
import { HttpClientModule } from '@angular/common/http';
import { TicketOrderComponent } from './shared/ticket-order/ticket-order.component';
import { TicketOrderCalendarComponent } from './shared/ticket-order-calendar/ticket-order-calendar.component';
import { HttpErrorInterceptorProvider } from './HttpErrorInterceptor ';
import { ErrorPageComponent } from './error-page/error-page.component';

@NgModule({
    declarations: [
        AppComponent,
        FooterLoginComponent,
        FooterLogoutComponent,
        TicketTariffComponent,
        HomeComponent,
        NavbarComponent,
        ArticlesComponent,
        LoginComponent,
        VisitSdcComponent,
        PartnersComponent,
        TicketOrderComponent,
        TicketOrderCalendarComponent,
        ErrorPageComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule
    ],
    providers: [HttpErrorInterceptorProvider],
    bootstrap: [AppComponent]
})
export class AppModule { }
