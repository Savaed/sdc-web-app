import { Injectable } from '@angular/core';
import { Observable, EMPTY } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../services/auth.service';


@Injectable()
export class ExchangeTokenInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401 && error.headers.get('Token-Expired') === 'true') {
                    console.warn('A provided access token has expired.');
                    this.authService.exchangeToken(this.authService.accessToken, this.authService.refreshToken);
                }
                console.log('Debug only - ExchangeTokenInterceptor works');
                console.log('The exchange of expired token was successful.');

                return next.handle(request);
            })
        );
    }
}

// Http interceptor providers in outside-in order.
export const ExchangeTokenInterceptorProvider = [{ provide: HTTP_INTERCEPTORS, useClass: ExchangeTokenInterceptor, multi: true }];
