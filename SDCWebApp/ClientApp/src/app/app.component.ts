import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    private readonly welcomeMessage = 'The page you see is the frontend part of the project created as my portfolio. ' +
        'Note that mastering the frontend was not my main goal (I\'m looking for a job as a backend programmer), so the page code is not perfect and a ' +
        'few things may not work as expected. If you interest the rest of my project please see GitHub: https://github.com/Savaed/SDCWebApp. ' +
        'Additionally for API documentation see: https://google.pl\n\nIn the end, I have to add the page was created only for educational purposes and doesn\'t ' +
        'present any existent company. Any resemblance to real characters, places, and events is accidental ;)\n';

    constructor() {
        this.displayWelcomeBanner(this.welcomeMessage);

    }

    private displayWelcomeBanner(message: string) {
        console.log(`%cWelcome\n %c${message}`, 'font-size: 40px; color: #0072cf; margin-top: .3em;', 'font-size: 16px; margin-right: 2em;');
    }
}
