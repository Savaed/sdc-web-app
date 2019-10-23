import { Component, OnInit } from '@angular/core';
import { OrderResponse } from 'src/app/models/Order';
import { TicketOrderService } from '../ticket-order.service';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { TicketTariff } from 'src/app/models/TicketTariff';
import { Discount } from 'src/app/models/Discount';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';
import { DiscountService } from 'src/app/services/discount.service';
import { Ticket } from 'src/app/models/Ticket';
import { map } from 'rxjs/operators';
import * as jsPDF from 'jspdf';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-order-details',
    templateUrl: './order-details.component.html',
    styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {
    private ticketOrder = new BehaviorSubject<OrderResponse>(undefined);
    private ticketTariffs = new BehaviorSubject<TicketTariff[]>(undefined);
    private discounts = new BehaviorSubject<Discount[]>(undefined);

    constructor(private ticketOrderService: TicketOrderService,
        private discountService: DiscountService,
        private ticketTariffSrevice: TicketTariffService,
        private actiatedRoute: ActivatedRoute) { }

    ngOnInit() {
        // Check if navigate directly from order form (newOrder = true) or user typed URL based on given order id eg. from ticket order email confirmation.
        if (this.actiatedRoute.snapshot.queryParams.newOrder === true) {
            this.ticketOrder = this.ticketOrderService.order;
        } else {
            const orderId = this.actiatedRoute.snapshot.params.id as string;
            this.ticketOrderService.getOrder(orderId).subscribe(order => this.ticketOrder.next(order));
        }

        this.discountService.getAllDiscounts().subscribe(discounts => this.discounts.next(discounts));
        this.ticketOrder.subscribe(x => console.log(x));
        this.discounts.subscribe(z => console.log(z));
        this.ticketTariffSrevice.getRecentTicketTariffs().subscribe(tariffs => this.ticketTariffs.next(tariffs));
    }

    private getDiscount(ticket: Ticket): Observable<Discount> {
        const discountId = ticket._links.find(x => x.rel === 'Discount').href.split('/')[1];
        const discount = this.discounts.getValue().find(x => x.id === discountId);
        return new BehaviorSubject(discount).asObservable();
    }

    private getTicketTariff(ticket: Ticket): Observable<TicketTariff> {
        const ticketTariffId = ticket._links.find(x => x.rel === 'TicketTariff').href.split('/')[1];
        const ticketTariff = this.ticketTariffs.getValue().find(x => x.id === ticketTariffId);
        return new BehaviorSubject(ticketTariff).asObservable();
    }

    saveOrderAsPdf() {
        const pdf = new jsPDF({
            orientation: 'landscape'
        });
        const source = document.getElementById('orderedTickets');
        pdf.fromHTML(source);
        pdf.save('test.pdf');
    }
}
