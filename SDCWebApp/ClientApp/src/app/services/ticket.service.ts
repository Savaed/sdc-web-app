import { Injectable } from '@angular/core';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';
import { Ticket } from '../models/Ticket';

@Injectable({
    providedIn: 'root'
})
export class TicketService {
    private readonly ticketUrl = ServerUrl + '/tickets';

    constructor(private http: HttpClient) { }

    public getTicket(id: string): Observable<Ticket> {
        return this.http.get<ApiResponse<Ticket>>(`${this.ticketUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllTickets(): Observable<Ticket[]> {
        return this.http.get<ApiResponse<Ticket[]>>(this.ticketUrl).pipe(map(response => response.data));
    }

    public getCustomersTicket(customerId: string, ticketId: string): Observable<Ticket> {
        return this.http.get<ApiResponse<Ticket>>(`${ServerUrl}/customers/${customerId}/tickets/${ticketId}`).pipe(map(response => response.data));
    }

    public getAllCustomersTickets(customerId: string): Observable<Ticket[]> {
        return this.http.get<ApiResponse<Ticket[]>>(`${ServerUrl}/customers/${customerId}/tickets`).pipe(map(response => response.data));
    }
}
