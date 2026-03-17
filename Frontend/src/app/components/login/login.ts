import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  standalone: true,
  template: `
<div class="d-flex justify-content-center align-items-center vh-100 bg-light">
  <div class="card shadow p-4" style="width: 350px;">
    <h3 class="text-center mb-3">Login</h3>

    <input [(ngModel)]="username" class="form-control mb-3" placeholder="Username">
    <input [(ngModel)]="password" type="password" class="form-control mb-3" placeholder="Password">

    <button class="btn btn-primary w-100" (click)="login()">Login</button>

    <p class="text-center mt-3">
      <a routerLink="/register">Create Account</a>
    </p>
  </div>
</div>
`,
  imports: [FormsModule]
})
export class Login {
  username = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) { }

  login() {
    this.auth.login({ userName: this.username, password: this.password })
      .subscribe(() => this.router.navigate(['/products']));
  }
}
