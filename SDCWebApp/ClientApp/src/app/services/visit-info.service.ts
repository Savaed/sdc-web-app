import { Injectable } from '@angular/core';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Discount } from '../models/Discount';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';
import { VisitInfo } from '../models/VisitInfo';

@Injectable({
    providedIn: 'root'
})
export class VisitInfoService {

    private readonly visitInfoUrl = ServerUrl + '/info';

    constructor(private http: HttpClient) { }

    public getInfo(id: string): Observable<VisitInfo> {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllInfo(): Observable<VisitInfo[]> {
        return this.http.get<ApiResponse<VisitInfo[]>>(this.visitInfoUrl).pipe(map(response => response.data));
    }

    public getRecentInfo(): Observable<VisitInfo> {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/recent`).pipe(map(response => response.data));
    }

    public addInfo(info: VisitInfo): Observable<VisitInfo> {
        return this.http.post<ApiResponse<VisitInfo>>(this.visitInfoUrl, info).pipe(map(response => response.data));
    }

    public updateInfo(id: string, info: VisitInfo): Observable<VisitInfo> {
        return this.http.put<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`, info).pipe(map(response => response.data));
    }

    public deleteInfo(id: string): Observable<ApiResponse<{}>> {
        return this.http.delete<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`);
    }
}
