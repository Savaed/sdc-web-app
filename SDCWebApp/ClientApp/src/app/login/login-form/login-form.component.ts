import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { map } from 'rxjs/operators';
import { observable, Observable, Subject, BehaviorSubject } from 'rxjs';
import { ApiError } from '../../models/ApiResponse';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {
    public loginForm: FormGroup;
    private passwordValidPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!#$%&\'()*+,\-.\/:;<=>?@[\\\]^_`{|}~]).{0,}$/;
    private usernameValidaPattern = /^[a-z]+$/;
    private serverErrorResponse = new BehaviorSubject<string>(null);

    public get username() {
        return this.loginForm.get('username');
    }

    public get password() {
        return this.loginForm.get('password');
    }

    constructor(private formBuilder: FormBuilder, private accountService: AccountService, private router: Router) { }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.minLength(2), Validators.pattern(this.usernameValidaPattern)]],
            password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(this.passwordValidPattern)]]
        });
    }

    private login() {
        this.accountService.login(this.username.value, this.password.value)
            .subscribe(response => {
                if (response.error) {
                    this.serverErrorResponse.next(response.error.message);
                    console.log(response.error);
                }
            });

        this.serverErrorResponse.subscribe(error => console.log(error));
    }
}
