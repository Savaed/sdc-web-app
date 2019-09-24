import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponse } from '../models/ApiResponse';
import { VisitInfo } from '../models/VisitInfo';
import { catchError, retry } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({
        Accept: 'text/plain, application/json, */*',
        'Accept-Encoding': 'gzip, deflate, br',
        'Accept-Language': 'pl,en-US;q=0.9,en;q=0.8'
    })
};

@Injectable({
    providedIn: 'root'
})
export class VisitInfoService extends BaseService {
    private readonly baseInfoApiUrl = 'https://localhost:44350/api/info';

    constructor(http: HttpClient) {
        super(http);
    }

    public getRecentInfo() {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.baseInfoApiUrl}/recent`, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }

    public getAllInfo() {
        return this.http.get<ApiResponse<VisitInfo>>(this.baseInfoApiUrl, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }

    public getInfo(id: string) {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.baseInfoApiUrl}/${id}`, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }

    public saveInfo(newInfo: VisitInfo) {
        return this.http.post<ApiResponse<VisitInfo>>(this.baseInfoApiUrl, newInfo, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }

    public deleteInfo(id: string) {
        return this.http.delete<ApiResponse<VisitInfo>>(`${this.baseInfoApiUrl}/${id}`, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }

    public updateInfo(updatedInfo: VisitInfo, id: string) {
        return this.http.put<ApiResponse<VisitInfo>>(`${this.baseInfoApiUrl}/${id}`, updatedInfo, httpOptions)
            .pipe(
                retry(this.requestRetryMaxNumber),
                catchError(this.handleError));
    }
}
