import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { AuthGuard } from 'src/app/core/helpers/auth.guard';
import { CadenasComponent } from './components/cadenas.component';
import { CadenaDetailComponent } from './components/cadenaDetail.component';

export const cadenaRoutes: Routes = [
{ path: 'cadenas', component: CadenasComponent, data: { name: 'Cadenas' }, canActivate: [AuthGuard] },
{ path: 'cadenas/:slug/show', component: CadenaDetailComponent, data: { name: 'Detalle Cadena' }, canActivate: [AuthGuard] }
];
export const cadenaRouting:
    ModuleWithProviders = RouterModule.forChild(cadenaRoutes);
