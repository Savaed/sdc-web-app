import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorHandler } from '../interceptors/ErrorHandler';
import { Observable } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-error-page',
    templateUrl: './error-page.component.html',
    styleUrls: ['./error-page.component.scss']
})
export class ErrorPageComponent implements OnInit {
    private errorCode: number;
    private errorGeneralMessage: Observable<string>;
    private errorSpecificMessage: Observable<string>;

    constructor(private activatedRoute: ActivatedRoute, private errorHandler: ErrorHandler, private titleService: Title) {
        this.errorCode = this.activatedRoute.snapshot.params.errorStatusCode;
        this.titleService.setTitle(`Error - ${this.errorCode.toString()}`);
    }

    ngOnInit() {
        this.errorSpecificMessage = this.errorHandler.errorSpecificMessage;
        this.errorGeneralMessage = this.errorHandler.errorGeneralMessage;
    }
}
