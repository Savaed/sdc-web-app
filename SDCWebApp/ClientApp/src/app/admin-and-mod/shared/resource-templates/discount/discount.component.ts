import { Component, OnInit, Input } from '@angular/core';
import { Discount } from 'src/app/models/Discount';

@Component({
    selector: 'app-discount',
    templateUrl: './discount.component.html',
    styleUrls: ['./discount.component.scss']
})
export class DiscountComponent implements OnInit {
    @Input() public discount: Discount;

    constructor() { }

    ngOnInit() { }
}
