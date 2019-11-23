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
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            retry(this.maxRequestRetryingNumber),
            catchError((error: HttpErrorResponse) => {

                if (error.status === 401 && error.headers.get('Token-Expired') === 'true') {
                    return next.handle(request);
                }

                this.errorHandler.logError(error);
                this.errorHandler.redirectToErrorPage(error);
                return throwError(error);
            })
        );
    }
}

// Http interceptor providers in outside-in order.
export const HttpErrorInterceptorProvider = [{ provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }];
