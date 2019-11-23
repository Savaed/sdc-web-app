import { Injectable } from '@angular/core';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';
import { SightseeingGroup } from '../models/SightseeingGroup';
import { GroupInfo } from '../models/GroupInfo';

@Injectable({
    providedIn: 'root'
})
export class VisitGroupService {
    private readonly visitGroupsUrl = ServerUrl + '/groups';

    constructor(private http: HttpClient) { }

    public getVisitGroup(id: string): Observable<SightseeingGroup> {
        return this.http.get<ApiResponse<SightseeingGroup>>(`${this.visitGroupsUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllVisitGroups(): Observable<SightseeingGroup[]> {
        return this.http.get<ApiResponse<SightseeingGroup[]>>(this.visitGroupsUrl).pipe(map(response => response.data));
    }

    public getAvailableVisitGroups(): Observable<GroupInfo[]> {
        return this.http.get<ApiResponse<GroupInfo[]>>(`${this.visitGroupsUrl}/available-dates`).pipe(map(response => response.data));
    }
}
