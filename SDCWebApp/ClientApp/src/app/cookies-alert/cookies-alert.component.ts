import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-cookies-alert',
    templateUrl: './cookies-alert.component.html',
    styleUrls: ['./cookies-alert.component.scss']
})
export class CookiesAlertComponent implements OnInit {
    private readonly acceptCookieItemName = 'accept-cookies';

    public get isUserAcceptCookiPolicy(): boolean { return localStorage.getItem(this.acceptCookieItemName) !== null; }

    constructor(private toast: ToastrService) { }

    ngOnInit() { }

    public acceptCookies() {
        localStorage.setItem(this.acceptCookieItemName, 'true');
        this.toast.info('Cookies have been accepted.');
    }
}
