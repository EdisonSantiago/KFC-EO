import { enableProdMode, Input, Component, OnInit } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UtilityService } from 'src/app/core/services/utility.service';
import { Respuesta } from '../../evaluations/domain/respuesta';
import { RespuestaService } from '../../evaluations/services/respuesta.service';
import { ValorRespuesta } from '../../evaluations/domain/valor.respuesta';
enableProdMode();
@Component({
  selector: 'app-standardaction',
  templateUrl: 'standardAction.component.html'
})
export class StandardActionComponent implements OnInit {
  @Input() evaluationId: any;
  @Input() standardId: any;
  @Input() standard: any;

  public showDetail = false;
  public respuesta: Respuesta;

  constructor(
    public standardService: EstandardService,
    public evaluationService: EvaluationService,
    public respuestaService: RespuestaService,
    public notificationService: NotificationService,
    public utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.getRespuesta(this.standardId, this.evaluationId);
  }

  getRespuesta(standardId: any, evaluationId: any) {

    this.respuestaService.getRespuestaByStandard(standardId, evaluationId)
      .then( res => {
        this.respuesta = res;
      });
  }

  getStatus(): string {
    let returnValue: string;

    switch (this.respuesta.valor) {

    case ValorRespuesta.Nulo:
      returnValue = 'Sin Respuesta';
      break;
    case ValorRespuesta.NoAplica:
      returnValue = 'No Aplica';
      break;

    case ValorRespuesta.SiCumple:
      returnValue = 'Si Cumple';
      break;

    case ValorRespuesta.NoCumple:
      returnValue = 'No Cumple';
      break;

    default:
      returnValue = '';
      break;
    }

    return returnValue;
  }
  close(): void {
    this.showDetail = false;

  }

  changeStatus(): void {
    if (this.respuesta.valor === ValorRespuesta.Nulo) {
      this.respuestaService.saveRespuesta(this.respuesta).then(res => {
          this.respuesta.valor = ValorRespuesta.NoAplica;
      });
    }

    if (this.respuesta.valor === ValorRespuesta.NoAplica) {
      this.respuestaService.saveRespuesta(this.respuesta).then(res => {
          this.respuesta.valor = ValorRespuesta.SiCumple;
      });
    }

    if (this.respuesta.valor === ValorRespuesta.SiCumple) {
      this.respuestaService.saveRespuesta(this.respuesta).then(res => {
        this.respuesta.valor = ValorRespuesta.NoCumple;
        this.showDetail = true;
      });
    }

    if (this.respuesta.valor === ValorRespuesta.NoCumple) {
      this.respuestaService.saveRespuesta(this.respuesta).then(res => {
        this.respuesta.valor = ValorRespuesta.Nulo;
      });
    }
    this.respuestaService.saveRespuesta(this.respuesta).then(res => {
      this.respuesta.detalle = "Hola";
    });
    this.respuestaService.saveRespuesta(this.respuesta);
  }
}
