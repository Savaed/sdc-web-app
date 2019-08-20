import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FooterLoginComponent,
    FooterLogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
