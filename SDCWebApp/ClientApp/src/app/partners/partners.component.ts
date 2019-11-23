import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-partners',
    templateUrl: './partners.component.html',
    styleUrls: ['./partners.component.scss']
})
export class PartnersComponent implements OnInit {
    private readonly title = 'Partnership';
    public now: Date;

    constructor(private titleService: Title) {
        this.titleService.setTitle(this.title);
     }

    ngOnInit() {
        this.now = new Date();
    }
}
