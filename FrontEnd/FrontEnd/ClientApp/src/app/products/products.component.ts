import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'protractor';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    constructor(
        private http: HttpClient,
        private Router: Router) { }
    public products: Product[];
    ngOnInit(): void {
        this.GetAllProducts();
    }
    public GetAllProducts() {
        this.http.get<Product[]>(environment.apiExam + 'GetProducts')
            .subscribe(result => {
                this.products = result;
            });
    }
}
interface Product {
    idProduct: number,
    nombre: string,
    costo: number,
    cantidadInventario: number,
}
