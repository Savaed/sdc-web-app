import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-visit-sdc',
    templateUrl: './visit-sdc.component.html',
    styleUrls: ['./visit-sdc.component.scss']
})
export class VisitSdcComponent implements OnInit {
    private readonly title = 'Visit us';

    constructor(private titleService: Title) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() {
    }
}
