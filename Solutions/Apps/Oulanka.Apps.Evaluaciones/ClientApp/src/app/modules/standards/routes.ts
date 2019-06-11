import { ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubstandardDetailComponent } from './components/substandardDetail.component';
import { AuthGuard } from 'src/app/core/helpers/auth.guard';

export const standardRoutes: Routes = [
{ path: 'estandares/:id/show', component: SubstandardDetailComponent, data: {name: 'Detalle Estandar'}, canActivate: [AuthGuard] }
];
export const standardRouting: ModuleWithProviders = RouterModule.forChild(standardRoutes);
