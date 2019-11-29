import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-cookies-alert',
    templateUrl: './cookies-alert.component.html',
    styleUrls: ['./cookies-alert.component.scss']
})
export class CookiesAlertComponent implements OnInit {
    private readonly acceptCookieItemName = 'accept-cookies';

    public get isUserAcceptCookiPolicy(): boolean { return localStorage.getItem(this.acceptCookieItemName) !== null; }

    constructor() { }

    ngOnInit() { }

    public acceptCookies() {
        localStorage.setItem(this.acceptCookieItemName, 'true');
    }
}
