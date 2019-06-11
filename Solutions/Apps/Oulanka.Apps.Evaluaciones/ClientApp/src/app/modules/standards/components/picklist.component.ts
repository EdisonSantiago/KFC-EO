import { enableProdMode, Input, Component, OnInit } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { standardRoutes } from '../routes';
import { Standard } from '../domain/standard';
import { Respuesta } from '../../evaluations/domain/respuesta';
import { RespuestaService } from '../../evaluations/services/respuesta.service';
import { ValorRespuesta } from '../../evaluations/domain/valor.respuesta';

enableProdMode();
@Component({
  selector: 'app-picklist',
  templateUrl: 'picklist.component.html'
})
export class PicklistComponent implements OnInit {
  @Input() standardId: any;
  @Input() evaluationId: any;
  @Input() standard: any;

  public showPicklist = false;
  public currentStandard: any;
  public items: Array<any>;
  public hasItems = false;
  public respuestas: Array<Respuesta> = new Array<Respuesta>();

  constructor(
    public standardService: EstandardService,
    public respuestaService: RespuestaService,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    if (this.standard !== undefined && !this.standard.hasChildren) {
      this.currentStandard = this.standard;
      this.getItems(this.currentStandard.id);
    }
  }

  getItems(standardId: any): void {
      this.standardService.getPicklist(standardId)
      .then((pickItems: Array<Standard>) => {
        const data: any = pickItems;
        this.items = data;
        if (this.items !== undefined) {
            this.hasItems = true;

            this.items.forEach(item => {
              this.getRespuestas(item.id);
            });
        }
      });
  }

  getRespuestaValue(standardId: any) {
    const item: Respuesta = this.respuestas.find((resp: Respuesta) => resp.estandarId === standardId);
    return item.valor;
  }

  getRespuestas(standardId: any): void {
    this.respuestaService.getRespuestaByStandard(standardId, this.evaluationId)
    .then( res => {
      const respuesta: Respuesta = res;
      this.respuestas.push(respuesta);
    });
  }

  saveValue(standardId: any): void {
    const respuesta: Respuesta = this.respuestas.find((resp: Respuesta) => resp.estandarId === standardId);
    if (respuesta.valor === ValorRespuesta.Nulo) {
      respuesta.valor = ValorRespuesta.NoAplica;
    }
    if (respuesta.valor === ValorRespuesta.NoAplica){
      respuesta.valor = ValorRespuesta.SiCumple;
    }
    if (respuesta.valor === ValorRespuesta.SiCumple) {
      respuesta.valor = ValorRespuesta.NoCumple;
    }
    if (respuesta.valor === ValorRespuesta.NoCumple) {
      respuesta.valor = ValorRespuesta.Nulo;
    }
    this.respuestaService.saveRespuesta(respuesta);
  }
}
