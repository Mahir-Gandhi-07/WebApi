import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Products } from './components/products/products';
import { Cart } from './services/cart';
import { Profile } from './components/profile/profile';
import { AdminDashboard } from './components/admindashboard/admindashboard';
import { AdminGuard } from './guards/admin-guard';
import { AuthGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'products', component: Products, canActivate: [AuthGuard] },
  { path: 'cart', component: Cart, canActivate: [AuthGuard] },
  { path: 'profile', component: Profile, canActivate: [AuthGuard] },
  { path: 'admin', component: AdminDashboard, canActivate: [AdminGuard] },
];
