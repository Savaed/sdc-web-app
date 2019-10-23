import { Injectable } from '@angular/core';
import { ErrorHandler } from './ErrorHandler';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';

const IsDevelopment = true;

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    private readonly maxRequestRetryingNumber = 0;

    constructor(private errorHandler: ErrorHandler) { }

    // Handles every error that occurs when requesting the API.
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            retry(this.maxRequestRetryingNumber),
            catchError((error: HttpErrorResponse) => {
                this.errorHandler.logError(error);
                this.errorHandler.redirectToErrorPage(error);
                return throwError(error);
            })
        );
    }
}

// Http interceptor providers in outside-in order.
export const HttpErrorInterceptorProvider = [{ provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }];
