import { Injectable } from '@angular/core';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Discount } from '../models/Discount';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';
import { VisitInfo, OpeningHours } from '../models/VisitInfo';

@Injectable({
    providedIn: 'root'
})
export class VisitInfoService {

    private readonly visitInfoUrl = ServerUrl + '/info';

    constructor(private http: HttpClient) { }

    public getInfo(id: string): Observable<VisitInfo> {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`).pipe(map(response => {
            this.sortDay(response.data.openingHours);
            return response.data;
        }));
    }

    public getAllInfo(): Observable<VisitInfo[]> {
        return this.http.get<ApiResponse<VisitInfo[]>>(this.visitInfoUrl).pipe(map(response => {
            response.data.forEach(info => {
                this.sortDay(info.openingHours);
            });
            return response.data;
        }));
    }

    public getRecentInfo(): Observable<VisitInfo> {
        return this.http.get<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/recent`).pipe(map(response => {
            this.sortDay(response.data.openingHours);
            return response.data;
        }));
    }

    public addInfo(info: VisitInfo): Observable<VisitInfo> {
        return this.http.post<ApiResponse<VisitInfo>>(this.visitInfoUrl, info).pipe(map(response => {
            this.sortDay(response.data.openingHours);
            return response.data;
        }));
    }

    public updateInfo(id: string, info: VisitInfo): Observable<VisitInfo> {
        return this.http.put<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`, info).pipe(map(response => {
            this.sortDay(response.data.openingHours);
            return response.data;
        }));
    }

    public deleteInfo(id: string): Observable<ApiResponse<{}>> {
        return this.http.delete<ApiResponse<VisitInfo>>(`${this.visitInfoUrl}/${id}`);
    }


    private sortDay(openingHours: OpeningHours[]) {
        const sorter = {
            monday: 1,
            tuesday: 2,
            wednesday: 3,
            thursday: 4,
            friday: 5,
            saturday: 6,
            sunday: 7
        };

        openingHours.sort((a, b) => {
            const day1 = a.dayOfWeek.toString().toLowerCase();
            const day2 = b.dayOfWeek.toString().toLowerCase();
            return sorter[day1] - sorter[day2];
        });
    }
}
