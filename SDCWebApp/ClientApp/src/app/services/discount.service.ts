import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServerUrl } from '../helpers/Constants';
import { Observable } from 'rxjs';
import { Discount } from '../models/Discount';
import { ApiResponse } from '../models/ApiResponse';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class DiscountService {
    private readonly discountUrl = ServerUrl + '/discounts';

    constructor(private http: HttpClient) { }

    public getDiscount(id: string): Observable<Discount> {
        return this.http.get<ApiResponse<Discount>>(`${this.discountUrl}/${id}`).pipe(map(response => response.data));
    }

    public getAllDiscounts(): Observable<Discount[]> {
        return this.http.get<ApiResponse<Discount[]>>(this.discountUrl).pipe(map(response => response.data));
    }

    public addDiscount(discount: Discount): Observable<Discount> {
        return this.http.post<ApiResponse<Discount>>(this.discountUrl, discount).pipe(map(response => response.data));
    }

    public updateDiscount(id: string, discount: Discount): Observable<Discount> {
        return this.http.put<ApiResponse<Discount>>(`${this.discountUrl}/${id}`, discount).pipe(map(response => response.data));
    }

    public deleteDiscount(id: string): Observable<ApiResponse<{}>> {
        return this.http.delete<ApiResponse<Discount>>(`${this.discountUrl}/${id}`);
    }
}
