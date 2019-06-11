import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Evaluation } from '../domain/evaluation';
import { EvaluationService } from '../services/evaluation.service';
import { NotificationService } from '../../../core/services/notification.service';
import { UtilityService } from '../../../core/services/utility.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { LocalService } from '../../locals/services/local.service';
import { DataService } from 'src/app/core/services/data.service';
import { Respuesta } from '../../evaluations/domain/respuesta';
import { GrupoStandard } from "../../standards/domain/grupoStandard";
import { EstandardService } from "../../standards/services/standard.service";
import { RespuestaService } from '../../evaluations/services/respuesta.service';
import { Standard } from "../../standards/domain/standard";
import { Estado } from "../../standards/domain/estado";

import { Resultado } from "../../standards/domain/resultado";
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { ChartType, ChartOptions } from 'chart.js';
import { Label } from 'ng2-charts';

export interface PeriodicElement {
  categoria: string;
  l1: string;
  l2: string;
  l3: string;
  result: string;

}


const ELEMENT_DATA: PeriodicElement[] = [
  {
    categoria: 'Brand Standards',
    l1: 'Los muebles, los letreros/anuncios, los equipos y el terreno del exterior están limpios y en buen estado.',
    l2: 'H',
    l3: 'Cleaning System, Repairs & Maintenance, Other. LSA',
    result: '1'
  },
];

@Component({
    selector: 'app-evaldetail',
    providers: [EvaluationService, NotificationService],
    templateUrl: './evaluationResumen.component.html'
})
export class EvaluationResumenComponent implements OnInit {


  public items: Array<Standard>;
  public items2: Array<Standard>;
  public items3: Array<Standard>;
  public estado: Array<Estado>;

    public evaluation: Evaluation;
    public evaluationId: any;
    public local: any;
    public sub: Subscription;
  public cadenaId: any;
  public pieChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      position: 'top',
    },
    plugins: {
      datalabels: {
        formatter: (value, ctx) => {
          const label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };

  public hasGroups = false;
  public groups: GrupoStandard[] = [];
  public selectedGroup: GrupoStandard;
  public respuesta: Respuesta;

 public l1 = 0;
public l2 = 0;
public l3 = 0;
  public pieChartLabels: Label[] = [['L1'], ['L2'], 'L3'];
  public pieChartData: number[] = [this.l1, this.l2,this.l3];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartColors = [
    {
      backgroundColor: ['rgba(255,0,0,0.3)', 'rgba(0,255,0,0.3)', 'rgba(0,0,255,0.3)'],
    },
  ];
  displayedColumns: string[] = ['categoria', 'l1', 'l2', 'l3', 'result'];
  dataSource = ELEMENT_DATA;

    constructor(
      public standardService: EstandardService,
      private apiEndpoint: ApiEndpointService,
        public localService: LocalService,
        public evaluationService: EvaluationService,
        public notificationService: NotificationService,
      public respuestaService: RespuestaService,

      private route: ActivatedRoute,

        private router: Router) { }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.evaluationId = params.id;
            this.cadenaId = localStorage.getItem('cadena');
          this.getItem();

          if (this.evaluationId !== undefined && this.cadenaId !== undefined) {
            this.ObteniendoLosGruposEstandares(this.cadenaId);
          }

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
                      this.items2 = data;
                      if (this.items2 !== undefined) {
                        for (var c = 0; c < this.items2.length; c++) {
                          this.respuestaService.getRespuestaByStandard(this.items2[c].id, this.evaluationId)
                            .then(res => {
                              this.respuesta = res;
                              if (this.respuesta.detalle == "L1") {
                                this.l1++;
                              }
                              if (this.respuesta.detalle == "L2") {
                                this.l2++;
                              }
                              if (this.respuesta.detalle == "L3") {
                                this.l3++;

                              }
                              this.pieChartData = [this.l1, this.l2, this.l3];
                              console.log(this.pieChartData);
                            });
                        }
                      }
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
