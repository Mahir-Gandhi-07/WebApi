import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';

interface Product {
  productId?: number;
  productName: string;
  description?: string;
  price: number;
  productImg?: string;
}

@Component({
  standalone: true,
  selector: 'app-admin-dashboard',
  template: `
<div class="container mt-4">
  <h2>Admin Dashboard</h2>

  <div class="row mt-4">
    <div class="col-md-4">
      <div class="card text-center shadow">
        <div class="card-body">
          <h5>Total Users</h5>
          <h3>--</h3>
        </div>
      </div>
    </div>

    <div class="col-md-4">
      <div class="card text-center shadow">
        <div class="card-body">
          <h5>Total Orders</h5>
          <h3>--</h3>
        </div>
      </div>
    </div>
  </div>
</div>
`,
  styles: [`
    .product-card { border: 1px solid #ccc; padding: 10px; margin: 5px; width: 200px; display: inline-block; vertical-align: top; }
    img { display: block; margin-bottom: 5px; }
    form { margin-bottom: 20px; }
    form input, form textarea { display: block; margin-bottom: 5px; width: 300px; }
  `],
  imports: [CommonModule, FormsModule]
})
export class AdminDashboard implements OnInit {
  products: Product[] = [];
  loading = true;
  currentProduct: Product = { productName: '', price: 0 };

  private apiUrl = 'https://localhost:7028/api/product';

  constructor(private http: HttpClient, private auth: AuthService, private router: Router) { }

  ngOnInit() {
    this.fetchProducts();
  }

  fetchProducts() {
    this.loading = true;
    this.http.get<Product[]>(this.apiUrl).subscribe({
      next: (res) => { this.products = res; this.loading = false; },
      error: (err) => { console.error(err); this.loading = false; }
    });
  }

  saveProduct() {
    if (this.currentProduct.productId) {
      // Update
      this.http.put(`${this.apiUrl}/${this.currentProduct.productId}`, this.currentProduct).subscribe({
        next: () => { this.fetchProducts(); this.currentProduct = { productName: '', price: 0 }; },
        error: (err) => console.error(err)
      });
    } else {
      // Add
      this.http.post(this.apiUrl, this.currentProduct).subscribe({
        next: () => { this.fetchProducts(); this.currentProduct = { productName: '', price: 0 }; },
        error: (err) => console.error(err)
      });
    }
  }

  editProduct(product: Product) {
    this.currentProduct = { ...product };
  }

  cancelEdit() {
    this.currentProduct = { productName: '', price: 0 };
  }

  deleteProduct(id: number) {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => this.fetchProducts(),
      error: (err) => console.error(err)
    });
  }

  logout() {
    this.auth.logout();
  }
}
