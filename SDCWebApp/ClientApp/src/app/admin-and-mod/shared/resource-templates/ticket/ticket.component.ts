import { Component, OnInit, Input } from '@angular/core';
import { Ticket } from 'src/app/models/Ticket';
import { Customer } from 'src/app/models/Customer';
import { Discount } from 'src/app/models/Discount';
import { TicketTariff } from 'src/app/models/TicketTariff';
import { CustomerService } from 'src/app/services/customer.service';
import { DiscountService } from 'src/app/services/discount.service';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';

@Component({
    selector: 'app-ticket',
    templateUrl: './ticket.component.html',
    styleUrls: ['./ticket.component.scss']
})
export class TicketComponent implements OnInit {
    @Input() public ticket: Ticket;
    public customer: Customer;
    public discount: Discount;
    public ticketTariff: TicketTariff;

    constructor(private customerService: CustomerService, private discountService: DiscountService, private ticketTariffService: TicketTariffService) { }

    ngOnInit() {
        this.ticketTariffService.getAllTicketTariffs().subscribe(tt => this.ticketTariff = tt.find(x => x.id === this.ticket._links.find(l => l.rel === 'TicketTariff').href.split('/')[1]));
        this.discountService.getDiscount(this.ticket._links.find(x => x.rel === 'Discount').href.split('/')[1]).subscribe(d => this.discount = d);
        this.customerService.getCustomer(this.ticket._links.find(x => x.rel === 'Customer').href.split('/')[1]).subscribe(c => this.customer = c);
    }
}
