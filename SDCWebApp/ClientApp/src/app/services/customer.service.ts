import { Injectable } from '@angular/core';
import { ServerUrl } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';
import { Customer } from '../models/Customer';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    private readonly customerUrl = ServerUrl + '/customers';

    constructor(private http: HttpClient) { }

    public getCustomer(id: string): Observable<Customer> {
        return this.http.get<ApiResponse<Customer>>(`${this.customerUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllCustomer(): Observable<Customer[]> {
        return this.http.get<ApiResponse<Customer[]>>(this.customerUrl).pipe(map(response => response.data));
    }
}
