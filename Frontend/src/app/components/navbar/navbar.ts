import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-navbar',
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4">
      <a class="navbar-brand fw-bold" routerLink="/products">MyStore</a>

      <button class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#nav">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div id="nav" class="collapse navbar-collapse">
        <ul class="navbar-nav me-auto">
          <li class="nav-item">
            <a class="nav-link" routerLink="/products">Products</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" routerLink="/Cart">Cart</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" routerLink="/profile">Profile</a>
          </li>
          <li class="nav-item" *ngIf="isAdmin()">
            <a class="nav-link" routerLink="/admin">Admin</a>
          </li>
        </ul>

        <button class="btn btn-danger" (click)="logout()">Logout</button>
      </div>
    </nav>
  `,
  imports: [RouterModule, FormsModule, CommonModule, RouterLink]
})
export class Navbar {
  constructor(private auth: AuthService) { }

  logout() {
    this.auth.logout();
  }

  isAdmin() {
    return this.auth.getRole() === 'admin';
  }
}
