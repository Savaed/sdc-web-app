import { Component, OnInit, Input } from '@angular/core';
import { TicketTariff } from 'src/app/models/TicketTariff';

@Component({
    selector: 'app-ticket-tariff',
    templateUrl: './ticket-tariff.component.html',
    styleUrls: ['./ticket-tariff.component.scss']
})
export class TicketTariffComponent implements OnInit {
    @Input() public ticketTariff: TicketTariff;

    constructor() { }

    ngOnInit() {
    }
}
