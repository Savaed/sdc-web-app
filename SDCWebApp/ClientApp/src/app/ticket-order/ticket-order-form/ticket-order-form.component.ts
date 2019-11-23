import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { TicketOrderService } from '../ticket-order.service';
import { Discount } from '../../models/Discount';
import { Subject, BehaviorSubject } from 'rxjs';
import { VisitGroupService } from 'src/app/services/visit-group.service';
import { Customer } from '../../models/Customer';
import { GroupInfo } from 'src/app/models/GroupInfo';

@Component({
    selector: 'app-ticket-order-form',
    templateUrl: './ticket-order-form.component.html',
    styleUrls: ['./ticket-order-form.component.scss']
})
export class TicketOrderFormComponent implements OnInit {
    private ticketOrderForm: FormGroup;
    private availableGroupDates = new BehaviorSubject<GroupInfo[]>(undefined);
    private firstAvailableGroupDate = new Subject<Date>();
    private lastAvailableGroupDate = new Subject<Date>();
    private firstDayInMonth = new Subject<number>();
    private daysInMonth = new Subject<number>();
    private customerDiscounts = new Array<Discount>();
    private isGroupButtonDisabled = new BehaviorSubject<boolean>(true);
    private groupInfo = new BehaviorSubject<GroupInfo[]>(new Array<GroupInfo>());
    private visitHour = new BehaviorSubject<Date>(undefined);
    private maxTicketsNumber = new BehaviorSubject<number>(0);

    constructor(private formBuilder: FormBuilder,
        private ticketOrderService: TicketOrderService,
        private groupsSerivce: VisitGroupService) {
    }

    private getGroupInfo(customerVisitDate: Date): GroupInfo[] {
        const groupInfo = new Array<GroupInfo>();

        if (this.availableGroupDates === undefined || this.availableGroupDates === null) {
            this.groupsSerivce.getAvailableVisitGroups().subscribe(gr => this.availableGroupDates.next(gr));
        }

        const groups = this.availableGroupDates.getValue();

        groups.forEach(group => {
            const visitDate = new Date(group.sightseeingDate);

            if (visitDate.getDay() === customerVisitDate.getDay() &&
                visitDate.getDate() === customerVisitDate.getDate() &&
                visitDate.getMonth() === customerVisitDate.getMonth() &&
                visitDate.getFullYear() === customerVisitDate.getFullYear()) {
                groupInfo.push(group);
            }
        });

        return groupInfo;
    }

    private setVisitHour(group: GroupInfo) {
        this.visitHour.next(group.sightseeingDate);
        this.maxTicketsNumber.next(group.availablePlace);
    }

    private onChanges() {
        this.visitDate.valueChanges.subscribe((value: Date) => {
            this.groupInfo.next(this.getGroupInfo(value));
            this.isGroupButtonDisabled.next(false);
            this.visitHour.next(undefined);
            this.maxTicketsNumber.next(0);
            this.numberOfTickets.setValue(0);
        });
    }

    private get email() { return this.customer.get('email') as FormControl; }
    private get birthdayDate() { return this.customer.get('birthdayDate') as FormControl; }
    private get visitDate() { return this.ticketOrderForm.get('visitDate') as FormControl; }
    private get customer() { return this.ticketOrderForm.get('customer') as FormGroup; }
    private get numberOfTickets() { return this.ticketOrderForm.get('numberOfTickets'); }

    ngOnInit() {
        this.ticketOrderForm = this.formBuilder.group({
            customer: this.formBuilder.group({
                email: ['', [Validators.email, Validators.required]],
                birthdayDate: ['']
            }),
            visitDate: ['', [Validators.required]],
            numberOfTickets: [0, [Validators.required, Validators.min(1)]]
        });

        this.onChanges();

        this.groupsSerivce.getAvailableVisitGroups().subscribe(groupDates => {
            this.availableGroupDates.next(groupDates);

        });

        if (this.availableGroupDates.getValue() !== undefined) {
            this.availableGroupDates.subscribe(dates => {
                this.firstAvailableGroupDate.next(dates[0].sightseeingDate);
                this.lastAvailableGroupDate.next(dates[dates.length - 1].sightseeingDate);
            });
        }

        this.firstAvailableGroupDate.subscribe(dateString => {
            const date = new Date(dateString);
            this.firstDayInMonth.next(new Date(date.getFullYear(), date.getMonth()).getDay());
            this.daysInMonth.next(this.calculateDaysInMonth(date.getMonth(), date.getFullYear()));
        });
    }

    private addDiscount(discount: Discount) {
        if (!this.customerDiscounts.includes(discount)) {
            this.customerDiscounts.push(discount);
        }
    }

    private addTickets() {
        const visitDate = new Date(this.visitDate.value);
        const visitTime = new Date(this.visitHour.getValue());
        const visitDateTime = new Date(visitDate.getFullYear(), visitDate.getMonth(), visitDate.getDate(), visitTime.getHours(), visitTime.getMinutes());
        this.ticketOrderService.addTicketToCart(this.customerDiscounts, visitDateTime, this.numberOfTickets.value);
        this.customerDiscounts = [];
        this.onChanges();
    }

    private orderTickets() {
        this.ticketOrderService.ticketOrderStep = 2;
        const customer: Customer = {
            hasFamilyCard: false,
            isChild: false,
            isDisabled: false,
            dateOfBirth: this.customer.get('birthdayDate').value,
            emailAddress: this.customer.get('email').value
        };

        this.ticketOrderService.orderTickets(customer).subscribe();
    }

    private calculateDaysInMonth(iMonth, iYear): number { return 32 - new Date(iYear, iMonth, 32).getDate(); }

    private filterGroupDates = (date: Date): boolean => {
        const day = date.getDay();
        const month = date.getMonth();
        const year = date.getFullYear();
        const monthDate = date.getDate();
        let isValidDate: boolean;

        const groups = this.availableGroupDates.getValue();

        isValidDate = groups.some(group => {
            const availableDate = new Date(group.sightseeingDate);
            return availableDate.getDay() === day &&
                availableDate.getDate() === monthDate &&
                availableDate.getMonth() === month &&
                availableDate.getFullYear() === year;
        });

        return isValidDate;
    }

    private filterBirthdayDates = (date: Date): boolean => {
        const now = new Date();
        const latestValidDate = new Date(now.getFullYear() - 126, now.getMonth(), now.getDay());

        return date > latestValidDate && date < now;
    }
}
