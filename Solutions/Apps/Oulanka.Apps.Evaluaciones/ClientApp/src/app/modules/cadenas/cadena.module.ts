import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';

import { UtilityService } from 'src/app/core/services/utility.service';
import { CadenaService } from './services/cadena.service';
import { cadenaRouting } from './routes';

import { CadenasComponent } from './components/cadenas.component';
import { CadenaDetailComponent } from './components/cadenaDetail.component';
import { CadenaSelectorComponent } from './components/cadenaSelector.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        cadenaRouting,
        SharedModule
    ],
    declarations: [
        CadenasComponent,
        CadenaDetailComponent,
        CadenaSelectorComponent
    ],
    providers: [
        DataService,
        CadenaService,
        NotificationService,
        UtilityService
    ],
    exports: [ CadenaSelectorComponent ]
})
export class CadenaModule { }
