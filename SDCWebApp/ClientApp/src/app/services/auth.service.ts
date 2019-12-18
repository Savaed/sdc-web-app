import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { LoginInfo } from '../models/LoginInfo';
import { User } from '../models/User';
import { ServerUrl } from '../helpers/Constants';
import { map } from 'rxjs/operators';
import '../helpers/DateExtension';
import { ExchangeToken } from '../models/ExchangeToken';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly accountManageUrl = ServerUrl + '/users';
    private jwtHelper: JwtHelperService;

    public get userRole() { return localStorage.getItem('user-role'); }
    public get username() { return localStorage.getItem('user-login'); }
    public get refreshToken() { return localStorage.getItem('refresh-token'); }
    public get accessToken() { return localStorage.getItem('access-token'); }

    // User only is logged when he has an access token and unxpired refresh token.
    public get isLogged(): BehaviorSubject<boolean> {
        const refreshToken = localStorage.getItem('refresh-token');

        if (localStorage.getItem('access-token') === null || refreshToken === null) {
            return new BehaviorSubject<boolean>(false);
        }

        const now = new Date();
        const isRefreshTokenExpired = new Date(localStorage.getItem('refresh-token-expiry-date')) < new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(),
            now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds());

        return new BehaviorSubject<boolean>(!isRefreshTokenExpired);
    }

    constructor(private http: HttpClient) {
        this.jwtHelper = new JwtHelperService();
    }

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
                    if (response.data !== null && response.data !== undefined && JSON.stringify(response.error) === '{}') {

                        // Store user's login, role and tokens data.
                        localStorage.setItem('user-login', response.data.user.userName);
                        localStorage.setItem('user-role', this.jwtHelper.decodeToken(response.data.accessToken.token).role);
                        this.saveTokensInLocalStorage(response.data);
                        console.log(`User '${username}' has been logged in.`);
                    }
                    return response;
                }));
    }

    public logout() {
        const username = this.username;
        localStorage.removeItem('user-login');
        localStorage.removeItem('user-role');
        localStorage.removeItem('access-token');
        localStorage.removeItem('access-token-expiry-date');
        localStorage.removeItem('refresh-token');
        localStorage.removeItem('refresh-token-expiry-date');
        console.log(`User '${username}' has been logged out.`);
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
