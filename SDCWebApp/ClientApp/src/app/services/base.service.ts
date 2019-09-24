import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService {
    protected readonly requestRetryMaxNumber = 2;
    private readonly notFoundErrorMessage = 'not found';
    private readonly internalServerErrorMessage = 'internal server error';

    constructor(protected http: HttpClient) { }

    protected handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(
                `Backend returned code ${error.status}, ` +
                `body was: ${error.error}`);
        }
        // return an observable with a user-facing error message
        return throwError(
            'Something bad happened; please try again later.');
    }

}
