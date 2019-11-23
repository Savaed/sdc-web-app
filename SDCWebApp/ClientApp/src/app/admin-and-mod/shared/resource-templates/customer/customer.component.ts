import { Component, OnInit, Input } from '@angular/core';
import { Customer } from 'src/app/models/Customer';
import { TicketService } from 'src/app/services/ticket.service';

@Component({
    selector: 'app-customer',
    templateUrl: './customer.component.html',
    styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
    @Input() public customer: Customer;
    public orderedTicketsIds = new Array<string>();

    constructor(private ticketService: TicketService) { }

    ngOnInit() {
        this.ticketService.getAllCustomersTickets(this.customer.id).subscribe(t => t.forEach(ticket => {
            this.orderedTicketsIds.push(ticket.id);
        }));
    }
}
