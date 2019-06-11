import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Evaluation } from '../domain/evaluation';
import { EvaluationService } from '../services/evaluation.service';
import { NotificationService } from '../../../core/services/notification.service';
import { UtilityService } from '../../../core/services/utility.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { LocalService } from '../../locals/services/local.service';
import { DataService } from 'src/app/core/services/data.service';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { GrupoStandard } from "../../standards/domain/grupoStandard";
import { EstandardService } from "../../standards/services/standard.service";
import { RespuestaService } from '../../evaluations/services/respuesta.service';
import { Standard } from "../../standards/domain/standard";
import { Resultado } from "../../standards/domain/resultado";

import { Respuesta } from '../../evaluations/domain/respuesta';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';


export interface PeriodicElement {
  seccion: string;
  ocurrionuevo: string;
  estandar: string;
  nivel: string;
  sistema: string;

}

const ELEMENT_DATA: PeriodicElement[] = [
  {
    seccion: 'Brand Standards',
    estandar: 'Los muebles, los letreros/anuncios, los equipos y el terreno del exterior están limpios y en buen estado.',
    nivel: 'H',
    sistema: 'Cleaning System, Repairs & Maintenance, Other. LSA',
    ocurrionuevo: '1'
  },


];

@Component({
    selector: 'app-evaldetail',
    providers: [EvaluationService, NotificationService],
    templateUrl: './evaluationPuntos.component.html'
})

export class EvaluationPuntosComponent implements OnInit {
    public evaluation: Evaluation;
    public evaluationId: any;
    public local: any;
    public hasItems = false;
    public resultado: Array<Resultado>;




  public items: Array<Standard>;
  public items2: Array<string>=[];
  public items3: Array<Standard>;


    public sub: Subscription;
    public cadenaId: any;
    public hasGroups = false;
    public groups: GrupoStandard[] = [];
    public selectedGroup: GrupoStandard;
  public respuesta: Respuesta;

  displayedColumns: string[] = ['seccion', 'estandar', 'nivel', 'sistema', 'ocurrionuevo'];
  dataSource = ELEMENT_DATA;
    constructor(
      public standardService: EstandardService,
      public catalogService: DataService,
      public localService: LocalService,
      public evaluationService: EvaluationService,
      public notificationService: NotificationService,
      private apiEndpoint: ApiEndpointService,
      public respuestaService: RespuestaService,
      private route: ActivatedRoute) {

    }

  async ngOnInit(): Promise<void> {
        this.sub = this.route.params.subscribe(params => {
            this.evaluationId = params.id;
            this.cadenaId = localStorage.getItem('cadena');
            this.getItem();
            if (this.evaluationId !== undefined && this.cadenaId !== undefined) {
            this.ObteniendoLosGruposEstandares(this.cadenaId);
            }
        });
    }
    getItem(): void {
        this.evaluationService.getBySlug(this.evaluationId)
            .then(res => {
                const data: any = res;
                this.evaluation = data;
                if ( this.evaluation !== undefined) {
                    this.getLocal(this.evaluation.localId);
                }
            }, error => {
                this.notificationService.printErrorMessage(error);
            });
    }

  ObteniendoLosGruposEstandares(grupoEstandarId: any): void {
    this.standardService.getGrupos().then((dbGroups: GrupoStandard[]) => {
      this.groups = dbGroups;
      if (this.groups !== undefined) {
        for (var a = 0; a < this.groups.length; a++) {
          this.standardService
            .getItems(this.groups[a].id)
            .then((dbStandards: Array<Standard>) => {
              const data: any = dbStandards;
              this.items = data;
              if (this.items !== undefined) {
                for (var b = 0; b < this.items.length; b++) {
                  this.standardService.getsubItems(this.items[b].id)
                    .then((subItems: Array<Standard>) => {
                      const data: any = subItems;
                      //this.items2 = data;
                      this.items2 = this.items2.concat(this.items2, data);
                      console.log(this.items2);

                      //this.items2.push.apply(this.items2,data);
                      /*if (this.items2 !== undefined) {
                        for (var c = 0; c < this.items2.length; c++) {
                          this.respuestaService.getRespuestaByStandard(this.items2[c].id, this.evaluationId)
                            .then(res => {
                              this.respuesta = res;
                              if (this.respuesta.valor == 3) {
                                //console.log(this.respuesta);
                              } 
                            });
                        }
                      }*/
                    });
                }

              }
            });
        }
        this.notificationService.printSuccessMessage('OK');
      } else {
        this.notificationService.printErrorMessage('No hay Grupos Disponibles');
      }
    });
  }

    getLocal(localId: any): void {
        this.localService.getById(localId)
        .then(res => {
            const data: any = res;
            this.local = data;
        }, error => {
            this.notificationService.printErrorMessage(error);
        });
    }

}
