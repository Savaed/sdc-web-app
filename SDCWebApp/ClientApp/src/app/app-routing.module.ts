import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FooterLoginComponent } from './footer-login/footer-login.component';
import { FooterLogoutComponent } from './footer-logout/footer-logout.component';
import { ArticlesComponent } from './articles/articles.component';
import { LoginComponent } from './login/login.component';
import { VisitSdcComponent } from './visit-sdc/visit-sdc.component';
import { PartnersComponent } from './partners/partners.component';
import { TicketOrderComponent } from './ticket-order/ticket-order.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { PricingComponent } from './pricing/pricing.component';
import { TicketOrderGuard } from './ticket-order/ticket-order.guard';
import { ArticleDetailsComponent } from './article-details/article-details.component';
import { OrderDetailsComponent } from './ticket-order/order-details/order-details.component';
import { OrderPaymentComponent } from './ticket-order/order-payment/order-payment.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'footer-login', component: FooterLoginComponent },
    { path: 'footer-logout', component: FooterLogoutComponent },
    { path: 'news', component: ArticlesComponent },
    { path: 'login', component: LoginComponent },
    { path: 'visit-sdc', component: VisitSdcComponent },
    { path: 'partners', component: PartnersComponent },
    { path: 'order', component: TicketOrderComponent, canActivate: [TicketOrderGuard], canLoad: [TicketOrderGuard] },
    { path: 'pricing', component: PricingComponent },
    { path: 'error/:errorStatusCode', component: ErrorPageComponent },
    { path: 'news/:articleName', component: ArticleDetailsComponent },
    { path: 'order/:id', component: OrderDetailsComponent },
    { path: 'payment', component: OrderPaymentComponent },
    { path: '**', pathMatch: 'full', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        anchorScrolling: 'enabled',
        onSameUrlNavigation: 'reload',
        scrollPositionRestoration: 'top'
    })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
