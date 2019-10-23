import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-footer-logout',
    templateUrl: './footer-logout.component.html',
    styleUrls: ['./footer-logout.component.scss']
})
export class FooterLogoutComponent implements OnInit {
    private currentYear = new Date().getFullYear();

    constructor() { }

    ngOnInit() {
    }

}
