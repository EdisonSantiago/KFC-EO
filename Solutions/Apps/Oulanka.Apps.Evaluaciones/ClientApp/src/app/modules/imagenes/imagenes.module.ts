import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UtilityService } from 'src/app/core/services/utility.service';
import { ImagenesService } from './services/imagenes.service';
import { ImagenesEvaluationComponent } from './components/imagenesevaluation.component';
import { ImagenesLocalComponent } from './components/imageneslocal.component';
import { ImagenesStandarComponent } from './components/imagenesstandar.component';

import { ImageDbInitComponent } from './components/imageDbInit.component';
import { ImageLocalUploadComponent } from './components/image-local-upload/image-local-upload.component';
import { ImageStandarUploadComponent } from './components/image-standar-upload/image-standar-upload.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule
    ],
    declarations: [
        ImagenesEvaluationComponent,
      ImagenesLocalComponent,
      ImagenesStandarComponent,
        ImageStandarUploadComponent,
        ImageDbInitComponent,
        ImageLocalUploadComponent
    ],
    providers: [
        DataService,
        NotificationService,
        UtilityService,
        ImagenesService
    ],
    exports: [
        ImagenesEvaluationComponent,
      ImagenesLocalComponent,
      ImagenesStandarComponent,
      ImageDbInitComponent,
      ImageStandarUploadComponent,
        ImageLocalUploadComponent
    ]
})
export class ImagenesModule { }
