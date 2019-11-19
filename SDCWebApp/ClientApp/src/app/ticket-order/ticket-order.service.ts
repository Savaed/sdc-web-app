import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ShallowTicket } from '../models/ShallowTicket';
import { Ticket } from '../models/Ticket';
import { VisitInfo } from '../models/VisitInfo';
import { TicketTariff } from '../models/TicketTariff';
import { Customer } from '../models/Customer';
import { Discount, DiscountType } from '../models/Discount';
import { TicketService } from 'src/app/services/ticket.service';
import { DiscountService } from 'src/app/services/discount.service';
import { HttpClient } from '@angular/common/http';
import { ServerUrl } from 'src/app/helpers/Constants';
import { ApiResponse } from '../models/ApiResponse';
import { OrderResponse, OrderRequest } from '../models/Order';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';


export class OrderedTicket {
    private orderTicket: {
        mostProfitableDiscount: Discount;
        ticketTariff: TicketTariff;
        visitDate: Date;
    };
    private amount: number;

    public get ticketAmount(): number { return this.amount; }

    public get ticket() { return this.orderTicket; }

    public get ticketPrice() {
        return this.ticket.ticketTariff.defaultPrice * (1 - (this.ticket.mostProfitableDiscount.discountValueInPercentage / 100));
    }

    constructor(ticketTariff: TicketTariff, discount: Discount, visitDate: Date, amount: number) {
        this.orderTicket = {
            ticketTariff,
            mostProfitableDiscount: discount,
            visitDate,
        };
        this.amount = amount;
    }
}


@Injectable({
    providedIn: 'root'
})
export class TicketOrderService {
    private readonly ticketOrderUrl = ServerUrl + '/orders';
    private customer = new BehaviorSubject<Customer>(undefined);
    private cart = new BehaviorSubject<OrderedTicket[]>([]);
    private currentTicketTariff = new BehaviorSubject<TicketTariff>(undefined);
    private recentInfo: VisitInfo;
    private orderStep = new BehaviorSubject<number>(0);
    private discounts = new BehaviorSubject<Discount[]>(new Array<Discount>());
    private commitedOrder = new BehaviorSubject<OrderResponse>(undefined);


    public get order(): BehaviorSubject<OrderResponse> { return this.commitedOrder; }

    public set choosenTicketTariff(value: TicketTariff) {
        this.currentTicketTariff.next(value);
        this.currentTicketTariff.subscribe(t => console.log(t));
    }

    public get ticketCart(): BehaviorSubject<OrderedTicket[]> { return this.cart; }

    public get choosenTicketTariff(): TicketTariff { return this.currentTicketTariff.getValue(); }

    public set ticketOrderStep(value: number) { this.orderStep.next(value); }

    public get ticketOrderStep(): number { return this.orderStep.getValue(); }

    constructor(private http: HttpClient, private discountService: DiscountService, private router: Router) {
        this.discountService.getAllDiscounts().subscribe(discounts => this.discounts.next(discounts));
        console.log('ticket order service ctor');

    }

    public addTicketToCart(customerChoosenDiscounts: Discount[], visitDate: Date, ticketsAmount: number) {
        // Calculate ticket's price based on the most profitable discount and choosed ticket tariff.
        // Add ticket to cart.

        const mostProfitableDiscount = customerChoosenDiscounts.length > 0 ? customerChoosenDiscounts.sort((a, b) => a > b ? -1 : 1)[0] : {
            type: -1,
            description: '',
            discountValueInPercentage: 0,
            groupSizeForDiscount: null,
        };
        const newTicket = new OrderedTicket(this.currentTicketTariff.getValue(), mostProfitableDiscount as Discount, visitDate, ticketsAmount);
        const tmpCart = this.cart.getValue();
        tmpCart.push(newTicket);
        this.cart.next(tmpCart);
        console.log(this.cart.getValue());
    }

    // Remove ticket from cart.
    public removeTicketFromCart(ticket: OrderedTicket) {
        const tmpCart = this.cart.getValue();
        const indexToRemove = tmpCart.findIndex(x => x.ticket === ticket.ticket);
        tmpCart.splice(indexToRemove, 1);
        this.cart.next(tmpCart);
    }

    // Cast ordered tickets from cart to shallow tickets and call ticket order API.
    public orderTickets(customer: Customer): Observable<OrderResponse> {
        const orderRequestBody: OrderRequest = {
            customer,
            tickets: this.toShallowTickets()
        };

        this.currentTicketTariff.next(undefined);
        this.cart.next([]);

        return this.http.post<ApiResponse<OrderResponse>>(this.ticketOrderUrl, orderRequestBody).pipe(map(response => {
            this.commitedOrder.next(response.data);
            console.log(this.commitedOrder.getValue());

            this.router.navigate(['payment']);
            setTimeout(() => {
                this.router.navigate(['/order', this.commitedOrder.getValue().id], { queryParams: { newOrder: true } });
            }, 15000);
            return response.data;
        }));
    }

    public getOrder(orderId: string): Observable<OrderResponse> {
        return this.http.get<ApiResponse<OrderResponse>>(`${this.ticketOrderUrl}/${orderId}`).pipe(map(response => response.data));
    }

    private toShallowTickets(): ShallowTicket[] {
        const cartTickets = this.ticketCart.getValue();
        console.log('toShallowTickets():', cartTickets);

        const shallowTickets: ShallowTicket[] = [];

        cartTickets.forEach(ticket => {
            for (let i = 0; i < ticket.ticketAmount; i++) {
                const tmpShallowTicket: ShallowTicket = {
                    discountId: ticket.ticket.mostProfitableDiscount.id,
                    sightseeingDate: ticket.ticket.visitDate,
                    ticketTariffId: ticket.ticket.ticketTariff.id
                };

                shallowTickets.push(tmpShallowTicket);
            }
        });

        return shallowTickets;
    }
}
