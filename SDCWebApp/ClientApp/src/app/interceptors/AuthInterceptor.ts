import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private readonly authorizationMethod = 'Bearer';

    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = this.authService.accessToken;

        if (accessToken !== null) {
            request = request.clone({
                setHeaders: { Authorization: `${this.authorizationMethod} ${accessToken}` }
            });
        }

        return next.handle(request);
    }
}

// Http interceptor providers in outside-in order.
export const AuthInterceptorProvider = [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }];
