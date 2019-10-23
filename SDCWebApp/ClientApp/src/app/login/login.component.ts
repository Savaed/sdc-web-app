import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    private readonly title = 'Login';

    constructor(private titleService: Title) {
        this.titleService.setTitle(this.title);
    }

    ngOnInit() { }
}
