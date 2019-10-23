import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ErrorHandler {
    private specificMessage = new BehaviorSubject<string>('');
    private generalMessage = new BehaviorSubject<string>('');

    public get errorGeneralMessage(): Observable<string> {
        return this.generalMessage.asObservable();
    }

    public get errorSpecificMessage(): Observable<string> {
        return this.specificMessage.asObservable();
    }

    constructor(private router: Router) { }

    // Log each error occured when requesting backend.
    public logError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {

            // A client-side or network error.
            console.error(`ERROR Message: ${error.error.error.message} Type: ${error.error.error.type}`);
        } else {

            // The backend returned an error response.
            console.error(`ERROR Message: ${error.error.error.message} StatusCode: ${error.error.error.statusCode} Type: ${error.error.error.type}`);
        }
    }

    // Redirect to specific error page.
    public redirectToErrorPage(error: HttpErrorResponse) {
        switch (error.status) {
            case 400:
                break;

            // Unauthorized http status code.
            case 401:
                this.generalMessage.next('Hey, where are you going? You cannot access this page because you are not logged in.');
                this.specificMessage.next('If you have an account, please turn to the login page and enter your login details.');
                this.router.navigate(['/error', error.status.toString()]);
                break;

            // Forbidden http status code.
            case 403:
                this.generalMessage.next('Hey, where are you going?. You cannot access this page because you do not have sufficient permissions.'
                    + 'Only administrators or moderators can have access to these resources.');
                this.router.navigate(['/error', error.status.toString()]);
                break;

            // Not found http status code.
            case 404:
                this.generalMessage.next('It looks like we can\'t find the page or resource you\'re looking for. '
                    + 'Please check that you typed the URL address correctly and try again later.');
                this.specificMessage.next('If you\'re an administrator or moderator, '
                    + 'make sure you\'re not trying to search for a resource that has been deleted, updated, or never existed.');
                this.router.navigate(['/error', error.status.toString()]);
                break;

            // Internal server error http status code.
            case 500:
                this.generalMessage.next('Something wrong happened on the server. '
                    + 'We will make every effort to ensure that this does not happen again.');
                this.specificMessage.next('If you are an administrator or moderator and this situation makes you unable to work, '
                    + 'be patient, remember that everything can be fixed.');
                this.router.navigate(['/error', error.status.toString()]);
                break;

            // Any other http error.
            default:
                this.generalMessage.next('Something wrong happened. We will make every effort to ensure that this does not happen again.');
                this.router.navigate(['/error', error.status.toString()]);
                break;
        }
    }
}
