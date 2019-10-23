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

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private readonly accountManageUrl = ServerUrl + '/users';

    public get isLogged(): BehaviorSubject<boolean> {
        //     const expiryIn = localStorage.getItem('access-token-expiry-date');
        //     return expiryIn != null ? new BehaviorSubject<boolean>(moment().isBefore(expiryIn)) : new BehaviorSubject<boolean>(false);
        return new BehaviorSubject<boolean>(moment.utc(moment.now()) < moment.utc(localStorage.getItem('access-token-expiry-date')));
    }

    constructor(private http: HttpClient) { }

    public login(username: string, password: string): Observable<ApiResponse<LoginInfo>> {
        return this.http.post<ApiResponse<LoginInfo>>(`${this.accountManageUrl}/login`, { username, password })
            .pipe(
                map(response => {
                    if (response.data != null && response.data !== undefined && JSON.stringify(response.error) === '{}') {

                        this.isLogged.next(true);

                        // Store user's login, access token and its expiry date.
                        localStorage.setItem('access-token', response.data.accessToken.token);
                        localStorage.setItem('access-token-expiry-date', response.data.accessToken.expiryIn.toString());
                        localStorage.setItem('user-login', response.data.user.userName);

                        // Store refresh token data.
                        localStorage.setItem('refresh-token', response.data.refreshToken.token);
                        localStorage.setItem('refresh-token-expiry-date', response.data.refreshToken.expiryIn.toString());

                        // TODO: Add user role to local storage.
                    }

                    return response;
                }));
    }

    // Registers a new user and returns login details or an error if registration failed. 
    // NOTE: This method is only available to administrators.
    public register(username: string, password: string, emailAddress: string, userRole: string): Observable<ApiResponse<User>> {
        throw new Error('Not implemented');
    }
}
