import { Component, OnInit, DoCheck } from '@angular/core';
import { TicketOrderService, OrderedTicket } from '../ticket-order.service';
import { BehaviorSubject } from 'rxjs';

@Component({
    selector: 'app-order-summary',
    templateUrl: './order-summary.component.html',
    styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit, DoCheck {
    public overallCartPrice = new BehaviorSubject<number>(0);
    public ticketCart = new BehaviorSubject<OrderedTicket[]>([]);
    public isCartEmpty = new BehaviorSubject<boolean>(true);

    constructor(private orderService: TicketOrderService) { }

    ngOnInit() {
        this.orderService.ticketCart.subscribe(cart => {
            this.overallCartPrice.next(this.calculateOveralPrice(cart));
            this.ticketCart.next(cart);
        });
    }

    ngDoCheck() {
        this.orderService.ticketCart.subscribe(cart => {
            this.overallCartPrice.next(this.calculateOveralPrice(cart));
            this.ticketCart.next(cart);
            this.isCartEmpty.next(cart.length === 0);
        });
    }

    public removeTicket(ticket: OrderedTicket) {
        this.orderService.removeTicketFromCart(ticket);
    }

    private calculateOveralPrice(tickets: OrderedTicket[]): number {
        let sum = 0;

        if (tickets === undefined) {
            return sum;
        }

        tickets.forEach(ticket => sum += (ticket.ticketPrice * ticket.ticketAmount));
        return sum;
    }
}
