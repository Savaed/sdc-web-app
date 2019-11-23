import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { ResourceOverviewComponent } from './shared/resource-overview/resource-overview.component';
import { AuthGuard } from '../guards/auth.guard';

const userRoutes: Route[] = [
    { path: 'over', component: ResourceOverviewComponent },
    {
        path: 'admin',
        canActivate: [AuthGuard],
        canLoad: [AuthGuard],
        children: [{ path: 'dashboard', component: DashboardComponent }]
    },
    {
        path: 'mod',
        canActivate: [AuthGuard],
        canLoad: [AuthGuard],
        children: [{ path: 'dashboard', component: DashboardComponent }]
    }
];

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        RouterModule.forChild(userRoutes)
    ],
    exports: [RouterModule]
})
export class AdminAndModRoutingModule { }
