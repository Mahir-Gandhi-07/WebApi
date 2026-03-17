import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  template: `
<div class="container mt-4">
  <h2 class="mb-4">Products</h2>

  <div class="row">
    <div class="col-md-3 mb-4" *ngFor="let p of products">
      <div class="card h-100 shadow-sm">

        <img *ngIf="p.productImg" [src]="p.productImg" class="card-img-top" style="height:180px; object-fit:cover;">

        <div class="card-body d-flex flex-column">
          <h5 class="card-title">{{p.productName}}</h5>
          <p class="card-text text-muted small">{{p.description}}</p>

          <div class="mt-auto">
            <h6 class="fw-bold">₹{{p.price}}</h6>

            <button class="btn btn-success w-100 mt-2" (click)="add(p.productId)">
              Add to Cart
            </button>
          </div>
        </div>

      </div>
    </div>
  </div>
</div>
`,
  imports: [CommonModule]
})
export class Products implements OnInit {
  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<any[]>('https://localhost:7028/api/product')
      .subscribe(res => this.products = res);
  }

  add(id: number) {
    this.http.post('https://localhost:7028/api/cart', { productId: id, quantity: 1 })
      .subscribe(() => alert('Added'));
  }
}
