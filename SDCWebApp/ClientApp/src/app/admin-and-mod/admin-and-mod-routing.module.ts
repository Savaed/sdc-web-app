import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { AuthGuard } from '../guards/auth.guard';

const userRoutes: Route[] = [
    {
        path: 'administrator',
        canActivate: [AuthGuard],
        canLoad: [AuthGuard],
        canActivateChild: [AuthGuard],
        children: [{ path: 'dashboard', component: DashboardComponent }]
    },
    {
        path: 'moderator',
        canActivate: [AuthGuard],
        canLoad: [AuthGuard],
        canActivateChild: [AuthGuard],
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
