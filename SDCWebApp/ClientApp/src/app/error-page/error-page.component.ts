import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorHandler } from '../ErrorHandler';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-error-page',
    templateUrl: './error-page.component.html',
    styleUrls: ['./error-page.component.scss']
})
export class ErrorPageComponent implements OnInit {
    private errorCode: number;
    private errorGeneralMessage: Observable<string>;
    private errorSpecificMessage: Observable<string>;

    constructor(private activatedRoute: ActivatedRoute, private errorHandler: ErrorHandler) { }

    ngOnInit() {
        this.errorCode = this.activatedRoute.snapshot.params.errorStatusCode;
        this.errorSpecificMessage = this.errorHandler.errorSpecificMessage;
        this.errorGeneralMessage = this.errorHandler.errorGeneralMessage;
    }
}
