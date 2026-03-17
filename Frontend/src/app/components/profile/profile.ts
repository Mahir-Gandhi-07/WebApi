import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  template: `
<div class="container mt-5">
  <div class="card shadow p-4 col-md-6 mx-auto">
    <h3 class="mb-3">My Profile</h3>

    <label>Username</label>
    <input [(ngModel)]="user.userName" class="form-control mb-3">

    <label>Email</label>
    <input [(ngModel)]="user.email" class="form-control mb-3">

    <button class="btn btn-primary w-100" (click)="update()">
      Update Profile
    </button>
  </div>
</div>
`,
  imports: [FormsModule]
})
export class Profile implements OnInit {
  user: any = {};

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get('https://localhost:7028/api/user/profile')
      .subscribe(res => this.user = res);
  }

  update() {
    this.http.put(`https://localhost:7028/api/user/updateuser/${this.user.userId}`, this.user)
      .subscribe(() => alert('Updated'));
  }
}
