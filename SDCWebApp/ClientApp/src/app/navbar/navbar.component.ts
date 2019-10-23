import { Component, OnInit, DoCheck } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, DoCheck {
    private openingHour = new BehaviorSubject<string>(undefined);
    private closingHour = new BehaviorSubject<string>(undefined);
    private isUserLogged = new BehaviorSubject<boolean>(false);

    get isLogged(): BehaviorSubject<boolean> { return this.accountService.isLogged; }

    constructor(private accountService: AccountService, private infoService: VisitInfoService, private router: Router) { }

    ngOnInit() {
        this.infoService.getRecentInfo().subscribe(info => {
            const now = new Date();
            this.openingHour.next(info.openingHours.find(hour => hour.dayOfWeek.toString() === now.dayToString()).openingHour.toString());
            this.closingHour.next(info.openingHours.find(hour => hour.dayOfWeek.toString() === now.dayToString()).closingHour.toString());
        });
    }

    ngDoCheck() {
        this.accountService.isLogged.subscribe(logged => this.isLogged.next(logged));
    }
}
