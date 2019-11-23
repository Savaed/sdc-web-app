import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Component({
    selector: 'app-order-payment',
    templateUrl: './order-payment.component.html',
    styleUrls: ['./order-payment.component.scss']
})
export class OrderPaymentComponent implements OnInit {
    public seconds = new BehaviorSubject<number>(15);

    constructor() { }

    ngOnInit() {
        setInterval(() => {
            if (this.seconds.getValue() > 0) {
                this.seconds.next(this.seconds.getValue() - 1);
            } else {
                return;
            }
        }, 1000);
    }
}
