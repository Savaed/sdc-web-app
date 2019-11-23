import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TicketTariff } from '../models/TicketTariff';
import { Discount } from '../models/Discount';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';
import { DiscountService } from 'src/app/services/discount.service';
import { TicketOrderService } from '../ticket-order/ticket-order.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-pricing',
    templateUrl: './pricing.component.html',
    styleUrls: ['./pricing.component.scss']
})
export class PricingComponent implements OnInit {
    private ticketTariffs = new Subject<TicketTariff[]>();
    private discounts = new Subject<Discount[]>();
    private readonly title = 'Pricing';

    constructor(private ticketTariffsService: TicketTariffService,
        private discountService: DiscountService,
        private orderService: TicketOrderService,
        private router: Router,
        private titleService: Title) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() {
        this.discountService.getAllDiscounts().subscribe(discounts => this.discounts.next(discounts));
        this.ticketTariffsService.getRecentTicketTariffs().subscribe(ticketTariffs => this.ticketTariffs.next(ticketTariffs.tariffs));
    }

    chooseTicketTariff(ticketTariff: TicketTariff) {
        this.orderService.choosenTicketTariff = ticketTariff;
        this.orderService.ticketOrderStep = 1;
        this.router.navigate(['/order']);
    }
}
