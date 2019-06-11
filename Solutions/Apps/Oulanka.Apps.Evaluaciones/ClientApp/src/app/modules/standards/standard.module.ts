import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UtilityService } from 'src/app/core/services/utility.service';

import { StandardGroupComponent } from './components/standardGroup.component';
import { Imagen } from './components/imagen.component';

import { EstandardService } from './services/standard.service';


import { StandarsComponent } from './components/standards.component';

import { SubstandarsComponent } from './components/substandars.component';


import { StandardActionComponent } from './components/standardAction.component';

import { SubstandardDetailComponent } from './components/substandardDetail.component';

import { standardRouting } from './routes';
import { StandardComponent } from './components/standard.component';

import { PicklistComponent } from './components/picklist.component';
import { ImagenDetalle } from './components/imagendetalle.component';


import { StandardDbInitComponent } from './components/standardDbInit.component';
import { ImagenesModule } from '../imagenes/imagenes.module';



@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
      SharedModule,
      ImagenesModule,
        standardRouting
    ],
    declarations: [

        StandardGroupComponent,
      Imagen,
        StandarsComponent,
        SubstandarsComponent,
        StandardActionComponent,
        SubstandardDetailComponent,
        StandardComponent,
      PicklistComponent,
      ImagenDetalle,
        StandardDbInitComponent
    ],
    providers: [
        DataService,
        NotificationService,
        UtilityService,
        EstandardService
    ],
    exports: [
        StandardGroupComponent,
      StandardGroupComponent,
      StandarsComponent,
      Imagen,
        SubstandarsComponent,
        StandardActionComponent,
        SubstandardDetailComponent,
      PicklistComponent,
      ImagenDetalle,
        StandardDbInitComponent
    ]
})
export class StandardModule { }
