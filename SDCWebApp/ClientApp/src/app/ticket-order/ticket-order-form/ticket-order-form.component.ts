import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { TicketOrderService } from '../ticket-order.service';
import { Discount } from '../../models/Discount';
import { Subject, BehaviorSubject } from 'rxjs';
import { VisitGroupService } from 'src/app/services/visit-group.service';
import { Customer } from '../../models/Customer';
import { GroupInfo } from 'src/app/models/GroupInfo';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-ticket-order-form',
    templateUrl: './ticket-order-form.component.html',
    styleUrls: ['./ticket-order-form.component.scss']
})
export class TicketOrderFormComponent implements OnInit {
    public ticketOrderForm: FormGroup;
    public availableGroupDates = new BehaviorSubject<GroupInfo[]>(undefined);
    public firstAvailableGroupDate = new Subject<Date>();
    public lastAvailableGroupDate = new Subject<Date>();
    public firstDayInMonth = new Subject<number>();
    public daysInMonth = new Subject<number>();
    public customerDiscounts = new Array<Discount>();
    public isGroupButtonDisabled = new BehaviorSubject<boolean>(true);
    public groupInfo = new BehaviorSubject<GroupInfo[]>(new Array<GroupInfo>());
    public visitHour = new BehaviorSubject<Date>(undefined);
    public maxTicketsNumber = new BehaviorSubject<number>(0);
    public clickedDiscountIndexes = new Array<number>();
    public isMobileLayout = new BehaviorSubject<boolean>(window.innerWidth <= 360);

    constructor(private formBuilder: FormBuilder,
        public ticketOrderService: TicketOrderService,
        private groupsSerivce: VisitGroupService,
        private toast: ToastrService) {
    }

    public get email() { return this.customer.get('email') as FormControl; }
    public get birthdayDate() { return this.customer.get('birthdayDate') as FormControl; }
    public get visitDate() { return this.ticketOrderForm.get('visitDate') as FormControl; }
    public get customer() { return this.ticketOrderForm.get('customer') as FormGroup; }
    public get numberOfTickets() { return this.ticketOrderForm.get('numberOfTickets'); }

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

    public setVisitHour(group: GroupInfo) {
        this.visitHour.next(new Date(group.sightseeingDate));
        this.maxTicketsNumber.next(group.availablePlace);
    }

    ngOnInit() {
        window.onresize = () => {
            this.isMobileLayout.next(window.innerWidth <= 360);
        };

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

    public addDiscount(discount: Discount, index: number) {
        if (!this.customerDiscounts.includes(discount)) {
            this.customerDiscounts.push(discount);
            this.clickedDiscountIndexes.push(index);
        } else if (this.customerDiscounts.includes(discount)) {
            this.customerDiscounts.splice(this.customerDiscounts.indexOf(discount), 1);
            this.clickedDiscountIndexes.splice(this.clickedDiscountIndexes.indexOf(index), 1);
        }
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

    public addTickets() {
        const visitDate = new Date(this.visitDate.value);
        const visitTime = new Date(this.visitHour.getValue());
        const visitDateTime = new Date(visitDate.getFullYear(), visitDate.getMonth(), visitDate.getDate(), visitTime.getHours(), visitTime.getMinutes());

        this.ticketOrderService.addTicketToCart(this.customerDiscounts, visitDateTime, this.numberOfTickets.value);
        this.customerDiscounts = [];
        this.clickedDiscountIndexes = [];
        this.onChanges();

        this.toast.info('New ticket has been added to the cart.');
    }

    public orderTickets() {
        this.ticketOrderService.ticketOrderStep = 2;
        const customer: Customer = {
            hasFamilyCard: false,
            isChild: false,
            isDisabled: false,
            dateOfBirth: this.customer.get('birthdayDate').value,
            emailAddress: this.customer.get('email').value
        };

        this.ticketOrderService.orderTickets(customer).subscribe();

        this.toast.info('The ticket order has been processed.');
    }

    private calculateDaysInMonth(iMonth, iYear): number { return 32 - new Date(iYear, iMonth, 32).getDate(); }

    public filterGroupDates = (date: Date): boolean => {
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

    public filterBirthdayDates = (date: Date): boolean => {
        const now = new Date();
        const latestValidDate = new Date(now.getFullYear() - 126, now.getMonth(), now.getDay());

        return date > latestValidDate && date < now;
    }
}
