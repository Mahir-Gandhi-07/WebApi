import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth';

export const AdminGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isLoggedIn() || auth.getRole() !== 'admin') {
    router.navigate(['/login']);
    return false;
  }
  return true;
};
