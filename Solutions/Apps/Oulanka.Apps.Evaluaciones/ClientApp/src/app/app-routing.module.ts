import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { accountRoutes, accountRouting } from './modules/account/routes';
import { evaluationRoutes, evaluationRouting} from './modules/evaluations/routes';

import { sharedRoutes, sharedRouting } from './modules/shared/routes';
import { AuthGuard } from './core/helpers/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    data: { name: 'Home' },
    canActivate: [AuthGuard]
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    data: { name: 'Dashboard' },
    canActivate: [AuthGuard]
  }
];

/*
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})*/
export const appRoutingModule: ModuleWithProviders = RouterModule.forRoot(routes);
