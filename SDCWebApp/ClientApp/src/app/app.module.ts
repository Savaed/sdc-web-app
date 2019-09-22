import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ArticlesComponent } from './articles/articles.component';
import { LoginComponent } from './login/login.component';
import { VisitSdcComponent } from './visit-sdc/visit-sdc.component';



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
    VisitSdcComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
