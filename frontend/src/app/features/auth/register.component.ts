import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  template: `
    <div class="mt-10">
      <h1 class="text-2xl font-bold text-center mb-6">Register</h1>
      @if (error) {
        <p class="text-red-600 text-sm text-center mb-4">{{ error }}</p>
      }
      <form (ngSubmit)="onSubmit()" class="space-y-4">
        <div>
          <label class="block text-sm font-medium mb-1">Username</label>
          <input type="text" [(ngModel)]="username" name="username" required minlength="3" maxlength="20"
            class="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-indigo-500 focus:outline-none">
        </div>
        <div>
          <label class="block text-sm font-medium mb-1">Email</label>
          <input type="email" [(ngModel)]="email" name="email" required
            class="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-indigo-500 focus:outline-none">
        </div>
        <div>
          <label class="block text-sm font-medium mb-1">Password</label>
          <input type="password" [(ngModel)]="password" name="password" required minlength="8"
            class="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-indigo-500 focus:outline-none">
        </div>
        <button type="submit" [disabled]="loading"
          class="w-full bg-indigo-600 text-white py-2 rounded-lg hover:bg-indigo-700 disabled:opacity-50">
          {{ loading ? 'Creating account...' : 'Register' }}
        </button>
      </form>
      <p class="text-center mt-4 text-sm">
        Already have an account? <a routerLink="/login" class="text-indigo-600 hover:underline">Login</a>
      </p>
    </div>
  `
})
export class RegisterComponent {
  username = '';
  email = '';
  password = '';
  error = '';
  loading = false;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    this.loading = true;
    this.error = '';
    this.authService.register({ username: this.username, email: this.email, password: this.password }).subscribe({
      next: () => {
        this.router.navigate(['/matches']);
      },
      error: (err) => {
        this.error = err.error?.error || 'Registration failed';
        this.loading = false;
      }
    });
  }
}
