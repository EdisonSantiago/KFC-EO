import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';


export const sharedRoutes: Routes = [];
export const sharedRouting:
    ModuleWithProviders = RouterModule.forChild(sharedRoutes);
