import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Evaluation } from '../domain/evaluation';
import { EvaluationService } from '../services/evaluation.service';
import { NotificationService } from '../../../core/services/notification.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { LocalService } from '../../locals/services/local.service';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { CatalogService } from 'src/app/core/services/catalog.service';

@Component({
    selector: 'app-evaldetail',
    providers: [EvaluationService, NotificationService],
    templateUrl: './evaluationDetail.component.html'
})
export class EvaluationDetailComponent implements OnInit {

    public evaluation: Evaluation;
    public evaluationId: any;
    public local: any;
    public sub: Subscription;
    public posiciones: Array<any>;
    public tiposVisita: Array<any>;
    public partesDia: Array<any>;
    public tiposEvaluacion: Array<any>;
    public cadenaId: any;

    constructor(
        public catalogService: CatalogService,
        public localService: LocalService,
        public evaluationService: EvaluationService,
        public notificationService: NotificationService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.evaluationId = params.id;
            this.cadenaId = localStorage.getItem('cadena');
            this.getCatalogoData();
            this.getItem();

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

    getCatalogoData(): void {

        this.catalogService.getPositions(this.cadenaId).then(
            res => {
                const data: any = res;
                this.posiciones = data;
            });

        this.catalogService.getTiposVisita().then(
            res => {
                const data: any = res;
                this.tiposVisita = data;
            });

        this.catalogService.getPartesDia().then(
            res => {
                const data: any = res;
                this.partesDia = data;
            });

        this.evaluationService.getTipos().then(
            res => {
              const data: any = res;
              this.tiposEvaluacion = data;
            }
        );
      }
}
