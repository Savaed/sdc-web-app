import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { LoginInfo } from '../models/LoginInfo';
import { User } from '../models/User';
import { ServerUrl } from '../helpers/Constants';
import { map } from 'rxjs/operators';
import * as moment from 'moment-mini-ts';
import '../helpers/DateExtension';
import { ExchangeToken } from '../models/ExchangeToken';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly accountManageUrl = ServerUrl + '/users';

    // User only is logged when he has an access token and unxpired refresh token.
    public get isLogged(): BehaviorSubject<boolean> {
        const refreshToken = localStorage.getItem('refresh-token');

        if (localStorage.getItem('access-token') === null || refreshToken === null) {
            return new BehaviorSubject<boolean>(false);
        }


        const isRefreshTokenExpired = new Date().toUtc(new Date(localStorage.getItem('refresh-token-expiry-date'))) < new Date();
        console.log('refresh token expired in: ', new Date(localStorage.getItem('refresh-token-expiry-date') + 'Z'));
        console.log('now', new Date());


        console.log(isRefreshTokenExpired);

        return new BehaviorSubject<boolean>(!isRefreshTokenExpired);
    }

    public get refreshToken() { return localStorage.getItem('refresh-token'); }

    public get accessToken() { return localStorage.getItem('access-token'); }

    constructor(private http: HttpClient) { }

    public exchangeToken(accessToken: string, refreshToken: string) {
        const newTokens = this.http.post<ApiResponse<ExchangeToken>>(`${ServerUrl}/auth/refresh-token`, { accessToken, refreshToken }).pipe(map(response => response.data));
        newTokens.subscribe(tokens => {
            this.saveTokensInLocalStorage(tokens);
        });
    }

    public login(username: string, password: string): Observable<ApiResponse<LoginInfo>> {
        return this.http.post<ApiResponse<LoginInfo>>(`${this.accountManageUrl}/login`, { username, password })
            .pipe(
                map(response => {
                    if (response.data != null && response.data !== undefined && JSON.stringify(response.error) === '{}') {

                        this.isLogged.next(true);

                        // Store user's login and toekns data.
                        localStorage.setItem('user-login', response.data.user.userName);
                        this.saveTokensInLocalStorage(response.data);

                        // TODO: Add user role to local storage.
                    }

                    return response;
                }));
    }

    public logout() {
        localStorage.removeItem('access-token');
        localStorage.removeItem('access-token-expiry-date');
        localStorage.removeItem('user-login');
        localStorage.removeItem('refresh-token');
        localStorage.removeItem('refresh-token-expiry-date');
    }

    // Registers a new user and returns login details or an error if registration failed.
    // NOTE: This method is only available to administrators.
    public register(username: string, password: string, emailAddress: string, userRole: string): Observable<ApiResponse<User>> {
        throw new Error('Not implemented');
    }

    private saveTokensInLocalStorage(tokens: LoginInfo | ExchangeToken) {
        // Store access token data.
        localStorage.setItem('access-token', tokens.accessToken.token);
        localStorage.setItem('access-token-expiry-date', tokens.accessToken.expiryIn.toString());

        // Store refresh token data.
        localStorage.setItem('refresh-token', tokens.refreshToken.token);
        localStorage.setItem('refresh-token-expiry-date', tokens.refreshToken.expiryIn.toString());
    }
}
