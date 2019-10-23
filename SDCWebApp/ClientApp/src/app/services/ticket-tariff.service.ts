import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../models/ApiResponse';
import { TicketTariff, TicketTariffJson } from '../models/TicketTariff';
import { ServerUrl } from '../helpers/Constants';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { VisitTariff } from '../models/VisitTariff';

@Injectable({
    providedIn: 'root'
})
export class TicketTariffService {

    constructor(private http: HttpClient) { }

    public getAllTicketTariffs(): Observable<TicketTariff[]> {
        return this.http.get<ApiResponse<TicketTariffJson[]>>(`${ServerUrl}/ticket-tariffs`)
            .pipe(
                map(response => response.data.map(ticketTariffJson => TicketTariff.mapToTicketTariff(ticketTariffJson)))
            );
    }

    public getRecentTicketTariffs(): Observable<TicketTariff[]> {
        return this.http.get<ApiResponse<VisitTariff>>(`${ServerUrl}/visit-tariffs/recent`)
            .pipe(
                map(response => response.data.ticketTariffs.map(ticketTariffJson => TicketTariff.mapToTicketTariff(ticketTariffJson)))
            );
    }

    public addTicketTariff(visitTariffId: string, ticketTariff: TicketTariff): Observable<TicketTariff> {
        // Map to JSON representation of ticket tariff object (with formated description).
        const ticketTariffJson = TicketTariff.mapFromTicketTariff(ticketTariff);

        return this.http.post<ApiResponse<TicketTariffJson>>(`${ServerUrl}/visit-tariffs/${visitTariffId}/ticket-tariffs`, ticketTariffJson)
            .pipe(
                map(response => TicketTariff.mapToTicketTariff(response.data))
            );
    }

    public updateTicketTariff(visitTariffId: string, ticketTariffId: string, ticketTariff: TicketTariff): Observable<TicketTariff> {
        // Map to JSON representation of ticket tariff object (with formated description).
        const ticketTariffJson = TicketTariff.mapFromTicketTariff(ticketTariff);

        return this.http.put<ApiResponse<TicketTariffJson>>(`${ServerUrl}/visit-tariffs/${visitTariffId}/ticket-tariffs/${ticketTariffId}`, ticketTariffJson)
            .pipe(
                map(response => TicketTariff.mapToTicketTariff(response.data))
            );
    }

    public deleteTicketTariff(visitTariffId: string, ticketTariffId: string): Observable<ApiResponse<{}>> {
        return this.http.delete<ApiResponse<{}>>(`${ServerUrl}/visit-tariffs/${visitTariffId}/ticket-tariffs/${ticketTariffId}`);
    }
}
