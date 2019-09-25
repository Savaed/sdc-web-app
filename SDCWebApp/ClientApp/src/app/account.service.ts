import { Injectable } from '@angular/core';
import { ErrorHandler } from './ErrorHandler';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from './shared/models/ApiResponse';
import { LoginInfo } from './shared/models/LoginInfo';
import { User } from './shared/models/User';
import { ServerUrl } from './Constants';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private readonly accountManageUrl = ServerUrl + '/users';

    constructor(private http: HttpClient, errorHandler: ErrorHandler) { }

    public login(username: string, password: string): Observable<ApiResponse<LoginInfo>> {
        throw new Error('Not implemented');
    }

    public register(username: string, password: string, emailAddress: string, userRole: string): Observable<ApiResponse<User>> {
        throw new Error('Not implemented');
    }
}
