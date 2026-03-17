import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  template: `
<div class="container mt-4">
  <h2 class="mb-4">My Cart</h2>

  <table class="table table-bordered shadow">
    <thead class="table-dark">
      <tr>
        <th>Product</th>
        <th>Quantity</th>
        <th>Action</th>
      </tr>
    </thead>

    <tbody>
      <tr *ngFor="let c of cart">
        <td>{{c.productName}}</td>
        <td>{{c.quantity}}</td>
        <td>
          <button class="btn btn-danger btn-sm" (click)="remove(c.cartId)">
            Remove
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</div>
`,
  imports: [CommonModule]
})
export class Cart implements OnInit {
  cart: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.http.get<any[]>('https://localhost:7028/api/cart')
      .subscribe(res => this.cart = res);
  }

  remove(id: number) {
    this.http.delete(`https://localhost:7028/api/cart/${id}`)
      .subscribe(() => this.load());
  }
}
