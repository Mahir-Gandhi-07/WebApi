import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = 'https://localhost:7028/api/auth';

  constructor(private http: HttpClient, private router: Router) { }

  login(data: any) {
    return this.http.post<any>(`${this.api}/login`, data).pipe(
      tap(res => localStorage.setItem('token', res.token))
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  getRole() {
    const token = localStorage.getItem('token');
    if (!token) return null;

    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.role || payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  }
}
