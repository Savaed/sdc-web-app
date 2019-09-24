import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { TicketTariffComponent } from './ticket-tariff/ticket-tariff.component';
import { HomeComponent } from './home/home.component';
import { ArticlesComponent } from './articles/articles.component';
import { LoginComponent } from './login/login.component';
import { VisitSdcComponent } from './visit-sdc/visit-sdc.component';
import { PartnersComponent } from './partners/partners.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';
import { TicketOrderCalendarComponent } from './ticket-order-calendar/ticket-order-calendar.component';


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
    { path: '**', pathMatch: 'full', redirectTo: 'home' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
