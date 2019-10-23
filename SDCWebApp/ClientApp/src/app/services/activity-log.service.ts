import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServerUrl } from '../helpers/Constants';
import { ActivityLog } from '../models/ActivityLog';
import { ApiResponse } from '../models/ApiResponse';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ActivityLogService {
    private readonly logsUrl = ServerUrl + '/logs';

    constructor(private http: HttpClient) { }

    public getLog(id: string): Observable<ActivityLog> {
        return this.http.get<ApiResponse<ActivityLog>>(`${this.logsUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllLogs(): Observable<ActivityLog[]> {
        return this.http.get<ApiResponse<ActivityLog[]>>(this.logsUrl).pipe(map(response => response.data));
    }
}
