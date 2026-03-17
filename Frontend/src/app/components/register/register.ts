import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  template: `
<div class="d-flex justify-content-center align-items-center vh-100 bg-light">
  <div class="card shadow p-4" style="width: 400px;">
    <h3 class="text-center mb-3">Register</h3>

    <input [(ngModel)]="userName" class="form-control mb-2" placeholder="Username">
    <input [(ngModel)]="email" class="form-control mb-2" placeholder="Email">
    <input [(ngModel)]="password" type="password" class="form-control mb-3" placeholder="Password">

    <button class="btn btn-success w-100" (click)="register()">Register</button>
  </div>
</div>
`,
  imports: [FormsModule]
})
export class Register {
  userName = '';
  email = '';
  password = '';

  constructor(private http: HttpClient) { }

  register() {
    this.http.post('https://localhost:7028/api/auth/register', this)
      .subscribe(() => alert('Registered'));
  }
}
