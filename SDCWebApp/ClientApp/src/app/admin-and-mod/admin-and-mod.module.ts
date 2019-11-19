import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { AdminAndModRoutingModule } from './admin-and-mod-routing.module';
import { ResourceOverviewComponent } from './shared/resource-overview/resource-overview.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ArticleComponent } from './shared/resource-templates/article/article.component';
import { ArticleFormComponent } from './shared/resource-templates/article/article-form/article-form.component';
import { TicketComponent } from './shared/resource-templates/ticket/ticket.component';
import { TicketTariffComponent } from './shared/resource-templates/ticket-tariff/ticket-tariff.component';
import { VisitInfoComponent } from './shared/resource-templates/visit-info/visit-info.component';
import { VisitGroupComponent } from './shared/resource-templates/visit-group/visit-group.component';
import { LogComponent } from './shared/resource-templates/log/log.component';
import { DiscountComponent } from './shared/resource-templates/discount/discount.component';
import { CustomerComponent } from './shared/resource-templates/customer/customer.component';
import { DiscountFormComponent } from './shared/resource-templates/discount/discount-form/discount-form.component';
import { TicketTariffFormComponent } from './shared/resource-templates/ticket-tariff/ticket-tariff-form/ticket-tariff-form.component';
import { VisitInfoFormComponent } from './shared/resource-templates/visit-info/visit-info-form/visit-info-form.component';
import { VisitTariffComponent } from './shared/resource-templates/visit-tariff/visit-tariff.component';
import { VisitTariffFormComponent } from './shared/resource-templates/visit-tariff/visit-tariff-form/visit-tariff-form.component';


@NgModule({
    declarations: [
        DashboardComponent,
        ResourceOverviewComponent,
        ArticleComponent,
        ArticleFormComponent,
        TicketComponent,
        TicketTariffComponent,
        VisitInfoComponent,
        VisitGroupComponent,
        LogComponent,
        DiscountComponent,
        CustomerComponent,
        DiscountFormComponent,
        TicketTariffFormComponent,
        VisitInfoFormComponent,
        VisitTariffComponent,
        VisitTariffFormComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatCheckboxModule,
        NgbModule,
        AdminAndModRoutingModule
    ]
})
export class AdminAndModModule { }
