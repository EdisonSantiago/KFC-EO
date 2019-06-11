import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { EvaluationsComponent } from './components/evaluations.component';
import { CreateEvaluationComponent } from './components/createEvaluation.component';
import { EvaluationDetailComponent } from './components/evaluationDetail.component';
import { AuthGuard } from 'src/app/core/helpers/auth.guard';
import { EvaluationCheckComponent } from './components/evaluationCheck.component';
import { EvaluationEquiposComponent } from './components/evaluationEquipos.component';
import { EvaluationImagenesComponent } from './components/evaluationImagenes.component';
import { EvaluationPuntosComponent } from './components/evaluationPuntos.component';
//import { EvaluationPreguntasComponent } from './components/evaluationPreguntas.component';
import { EvaluationResumenComponent } from './components/evaluationResumen.component';

export const evaluationRoutes: Routes = [
{ path: 'evaluaciones', component: EvaluationsComponent, data: { name: 'Evaluaciones' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/new', component: CreateEvaluationComponent, data: { name: 'Crear nueva Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/show', component: EvaluationDetailComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/check', component: EvaluationCheckComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/equipos', component: EvaluationEquiposComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/imagenes', component: EvaluationImagenesComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/puntos', component: EvaluationPuntosComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
//{ path: 'evaluaciones/:id/preguntas', component: EvaluationPreguntasComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] },
{ path: 'evaluaciones/:id/resumen', component: EvaluationResumenComponent, data: { name: 'Detalle Evaluacion' }, canActivate: [AuthGuard] }
];
export const evaluationRouting:
    ModuleWithProviders = RouterModule.forChild(evaluationRoutes);
