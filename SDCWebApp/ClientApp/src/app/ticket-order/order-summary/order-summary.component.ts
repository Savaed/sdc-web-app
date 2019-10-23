import { Component, OnInit, DoCheck } from '@angular/core';
import { TicketOrderService, OrderedTicket } from '../ticket-order.service';
import { BehaviorSubject } from 'rxjs';
import { Ticket } from 'src/app/models/Ticket';

@Component({
    selector: 'app-order-summary',
    templateUrl: './order-summary.component.html',
    styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit, DoCheck {
    private overallCartPrice = new BehaviorSubject<number>(0);
    private ticketCart = new BehaviorSubject<OrderedTicket[]>([]);
    private isCartEmpty = new BehaviorSubject<boolean>(true);

    constructor(private orderService: TicketOrderService) { }

    ngOnInit() {
        this.orderService.ticketCart.subscribe(cart => {
            this.overallCartPrice.next(this.calculateOveralPrice(cart));
            this.ticketCart.next(cart);
            console.log(this.isCartEmpty.getValue());
        });
    }

    ngDoCheck() {
        this.orderService.ticketCart.subscribe(cart => {
            this.overallCartPrice.next(this.calculateOveralPrice(cart));
            this.ticketCart.next(cart);
            this.isCartEmpty.next(cart.length === 0);
        });
    }

    private removeTicket(ticket: OrderedTicket) {
        this.orderService.removeTicketFromCart(ticket);
    }

    private calculateOveralPrice(tickets: OrderedTicket[]): number {
        let sum = 0;

        if (tickets === undefined) {
            return sum;
        }

        tickets.forEach(ticket => sum += (ticket.ticketPrice * ticket.ticketAmount));
        console.log(sum);
        return sum;
    }
}
