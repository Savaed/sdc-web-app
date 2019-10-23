import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TicketOrderService } from './ticket-order.service';

@Injectable({
    providedIn: 'root'
})
export class TicketOrderGuard implements CanActivate {

    constructor(private ticketOrderService: TicketOrderService, private router: Router) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.canNavigateToOrder();
    }

    private canNavigateToOrder() {
        if (this.ticketOrderService.choosenTicketTariff !== undefined) {
            return true;
        } else {
            console.warn('Cannot continue ordering tickets without selecting any ticket price list.');
            return this.router.parseUrl('/pricing');
        }
    }
}
