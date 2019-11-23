import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TicketOrderService } from './ticket-order.service';

@Component({
    selector: 'app-ticket-order',
    templateUrl: './ticket-order.component.html',
    styleUrls: ['./ticket-order.component.scss']
})
export class TicketOrderComponent implements OnInit {
    private readonly title = 'Ticket order';

    constructor(private titleService: Title, public orderService: TicketOrderService) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() { }
}
