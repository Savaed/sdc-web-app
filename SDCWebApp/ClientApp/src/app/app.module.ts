import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HttpErrorInterceptorProvider } from './interceptors/HttpErrorInterceptor ';
import { DiscountTypePipe } from './pipes/discount-type.pipe';
import { ArticlesComponent } from './articles/articles.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { LoginComponent } from './login/login.component';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';
import { TicketOrderFormComponent } from './ticket-order/ticket-order-form/ticket-order-form.component';
import { PartnersComponent } from './partners/partners.component';
import { OrderDetailsComponent } from './ticket-order/order-details/order-details.component';
import { OrderPaymentComponent } from './ticket-order/order-payment/order-payment.component';
import { OrderSummaryComponent } from './ticket-order/order-summary/order-summary.component';
import { VisitSdcComponent } from './visit-sdc/visit-sdc.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule, MatDatepickerModule } from '@angular/material';
import { PricingComponent } from './pricing/pricing.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CookiesAlertComponent } from './cookies-alert/cookies-alert.component';
import { ArticleDetailsComponent } from './article-details/article-details.component';
import { Title } from '@angular/platform-browser';
import { AuthInterceptorProvider } from './interceptors/AuthInterceptor';
import { ExchangeTokenInterceptorProvider } from './interceptors/ExchangeTokenInterceptor';
import { AdminAndModModule } from './admin-and-mod/admin-and-mod.module';
import { ToastComponent } from './toast/toast.component';


@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        FooterLoginComponent,
        FooterLogoutComponent,
        DiscountTypePipe,
        ArticlesComponent,
        ErrorPageComponent,
        LoginComponent,
        LoginFormComponent,
        HomeComponent,
        TicketOrderComponent,
        TicketOrderFormComponent,
        PartnersComponent,
        PricingComponent,
        OrderDetailsComponent,
        OrderPaymentComponent,
        OrderSummaryComponent,
        VisitSdcComponent,
        CookiesAlertComponent,
        ArticleDetailsComponent,
        ToastComponent
    ],
    imports: [
        HttpClientModule,
        BrowserModule,
        BrowserAnimationsModule,
        AdminAndModModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        NgbModule
    ],
    providers: [
        HttpErrorInterceptorProvider,
        ExchangeTokenInterceptorProvider,
        AuthInterceptorProvider,
        Title
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
