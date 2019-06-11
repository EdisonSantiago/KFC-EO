import { enableProdMode, Input, Component, OnInit } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Respuesta } from '../../evaluations/domain/respuesta';

import { NotificationService } from 'src/app/core/services/notification.service';
import { RespuestaService } from '../../evaluations/services/respuesta.service';
enableProdMode();
@Component({
    selector: 'app-imagen',
    templateUrl: 'imagen.component.html'
})
export class Imagen implements OnInit {
    @Input() standardId: any;
    @Input() evaluationId: any;
    @Input() standard: any;
  public showDetail = false;
  public respuesta: Respuesta;

    public showPicklist = false;
constructor(
    public standardService: EstandardService,
    public evaluationService: EvaluationService,
    private router: Router,

    public notificationService: NotificationService,
    public respuestaService: RespuestaService
) { }
  getRespuesta(standardId: any, evaluationId: any) {

    this.respuestaService.getRespuestaByStandard(standardId, evaluationId)
      .then( res => {
        this.respuesta = res;
      });
  }
  cerrar(): void {
  //  this.showPicklist = false;

    this.router.navigate(['evaluaciones',this.evaluationId,'resumen']);
  }
ngOnInit(): void {
    if (this.standard !== undefined && !this.evaluationId.hasChildren) {
      this.getRespuesta(this.standardId, this.evaluationId);

      this.showPicklist = true;
    } 
}
}
