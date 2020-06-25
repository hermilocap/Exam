import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};
@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    LoginForm: FormGroup;
    flagError: boolean = false;
    messageError: string;
    constructor(private http: HttpClient,
        private Route: Router,
        private FormBuilder: FormBuilder) {

    }
    ngOnInit() {
        this.LoginForm = this.FormBuilder.group({
            Nombre: ['', Validators.required],
            Password: ['', Validators.required]
        })
    }
    OnLogin() {
        this.loginuser(this.LoginForm.value);
    }
    public loginuser(user: User) {
        this.http.post(environment.apiExam + "Login", {
            "nombre": user.Nombre,
            "password": user.Password
        }, httpOptions).subscribe(
            result => {
                if (this.LoginForm.valid) {
                    console.log(result);
                    if (result == true) {
                        this.Route.navigate(["/app-products"]);
                    } else {
                        this.flagError = true;
                        this.messageError = "Usuario o contraseña invalidos";
                    }
                }
            }
        )
    };
}
