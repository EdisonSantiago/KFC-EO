import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';

import { UtilityService } from 'src/app/core/services/utility.service';
import { EvaluationService } from './services/evaluation.service';
import { evaluationRouting } from './routes';

import {  MatTableModule } from '@angular/material';

import { EvaluationsComponent } from './components/evaluations.component';
import { EvaluationDetailComponent } from './components/evaluationDetail.component';
import { CreateEvaluationComponent } from './components/createEvaluation.component';
import { SidebarComponent } from './components/sidebar.component';
import { EvaluationCheckComponent } from './components/evaluationCheck.component';
import { EvaluationResumenComponent } from './components/evaluationResumen.component';
import { EvaluationPuntosComponent } from './components/evaluationPuntos.component';
import { EvaluationImagenesComponent } from './components/evaluationImagenes.component';
import { EvaluationEquiposComponent } from './components/evaluationEquipos.component';
import { StandardModule } from '../standards/standard.module';
import { LocalModule } from '../locals/local.module';
import { EquipoModule } from '../equipos/equipo.module';
import { ImagenesModule } from '../imagenes/imagenes.module';
import { RespuestaService } from './services/respuesta.service';
import { EvaluationDbInitComponent } from './components/evaluationDbInit.component';
import { RespuestaDbInitComponent } from './domain/respuestaDbInit.component';

    import { ChartsModule } from 'ng2-charts';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        evaluationRouting,
        SharedModule,
        StandardModule,
        LocalModule,
        MatTableModule,
        EquipoModule,
       ChartsModule,

        ImagenesModule
    ],
    declarations: [
        EvaluationsComponent,
        EvaluationDetailComponent,
        CreateEvaluationComponent,
        SidebarComponent,
        EvaluationCheckComponent,
        EvaluationEquiposComponent,
        EvaluationImagenesComponent,
        EvaluationPuntosComponent,
        EvaluationResumenComponent,
        EvaluationDbInitComponent,
        RespuestaDbInitComponent
    ],
    providers: [
        DataService,
        EvaluationService,
        RespuestaService,
        NotificationService,
        UtilityService
    ],
    exports: [
        SidebarComponent,
        EvaluationDbInitComponent,
        RespuestaDbInitComponent
    ]
})
export class EvaluationModule { }
