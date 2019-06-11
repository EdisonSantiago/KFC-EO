import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccountComponent } from './components/account.component';
import { LoginComponent } from './components/login.component';
import { RegisterComponent } from './components/register.component';

export const accountRoutes: Routes = [
  {
    path: 'account',
    component: AccountComponent,
    children: [
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent }
    ]
  }
];

export const accountRouting:
  ModuleWithProviders = RouterModule.forChild(accountRoutes);
