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
  selector: 'app-imagendetalle',
  templateUrl: 'imagendetalle.component.html'
})
export class ImagenDetalle implements OnInit {
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
    
    case ValorRespuesta.NoCumple:
      returnValue = 'Ver Foto';
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
  imagen(): void {
    this.showDetail = true;
  }
}
