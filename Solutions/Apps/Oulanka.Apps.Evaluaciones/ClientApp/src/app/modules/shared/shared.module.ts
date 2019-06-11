import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { HeaderComponent } from './components/header/header.component';
import { LoaderComponent } from './components/loader/loader.component';

import { sharedRouting } from './routes';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        sharedRouting
    ],
    declarations: [
        BreadcrumbComponent,
        HeaderComponent,
        LoaderComponent
    ],
    exports: [
        BreadcrumbComponent,
        HeaderComponent,
        LoaderComponent
    ]
})
export class SharedModule { }
