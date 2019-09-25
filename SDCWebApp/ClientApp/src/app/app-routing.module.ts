import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FooterLoginComponent } from './shared/footer-login/footer-login.component';
import { FooterLogoutComponent } from './shared/footer-logout/footer-logout.component';
import { TicketTariffComponent } from './shared/ticket-tariff/ticket-tariff.component';
import { HomeComponent } from './shared/home/home.component';
import { ArticlesComponent } from './shared/articles/articles.component';
import { LoginComponent } from './shared/login/login.component';
import { VisitSdcComponent } from './shared/visit-sdc/visit-sdc.component';
import { PartnersComponent } from './shared/partners/partners.component';
import { TicketOrderComponent } from './shared/ticket-order/ticket-order.component';
import { TicketOrderCalendarComponent } from './shared/ticket-order-calendar/ticket-order-calendar.component';
import { ErrorPageComponent } from './error-page/error-page.component';


const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'ticket-tariff', component: TicketTariffComponent },
    { path: 'footer-login', component: FooterLoginComponent },
    { path: 'footer-logout', component: FooterLogoutComponent },
    { path: 'articles', component: ArticlesComponent },
    { path: 'login', component: LoginComponent },
    { path: 'visit-sdc', component: VisitSdcComponent },
    { path: 'partners', component: PartnersComponent },
    { path: 'ticket-order', component: TicketOrderComponent },
    { path: 'calendar', component: TicketOrderCalendarComponent },
    { path: 'error/:errorStatusCode', component: ErrorPageComponent },
    { path: '**', pathMatch: 'full', redirectTo: 'home' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
