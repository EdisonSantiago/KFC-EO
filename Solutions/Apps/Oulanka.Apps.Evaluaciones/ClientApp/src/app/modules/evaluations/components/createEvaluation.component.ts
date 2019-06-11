import { Component, OnInit } from '@angular/core';
import { EvaluationForm } from '../domain/evaluationForm';
import { OperationResult } from '../../../core/domain/operationResult';
import { NotificationService } from '../../../core/services/notification.service';
import { EvaluationService } from '../services/evaluation.service';
import { getLocaleDateFormat } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { FormBuilder } from '@angular/forms';
import { DataService } from 'src/app/core/services/data.service';
import { LocalService } from '../../locals/services/local.service';
import { removeSummaryDuplicates } from '@angular/compiler';

import { Local } from '../../locals/domain/local';

@Component({
  selector: 'app-createeval',
  providers: [EvaluationService, NotificationService],
  templateUrl: './createEvaluation.component.html'
})



export class CreateEvaluationComponent implements OnInit {
    public newEvaluation: EvaluationForm;
    public posiciones: Array<any>;
    public tiposVisita: Array<any>;
    public partesDia: Array<any>;
    public tiposEvaluacion: Array<any>;
    public cadenaId: any;
    public codigolocal: any;
    public loading:boolean;
    public selectedLocal: any;
    public showSelector = false;
  public showDetail = false;


  constructor(
    private formBuilder: FormBuilder,
    private catalogService: DataService,
    private evaluationService: EvaluationService,
    private notificationService: NotificationService,
    private localService: LocalService,
    private router: Router,
    private apiEndpoint: ApiEndpointService,
  ) {
  }
  close(): void {
   

  }
  ngOnInit() {
    this.showDetail = true;

    this.newEvaluation = new EvaluationForm(0, 0, 0, '', '', 0, 0, 0, 0);
    this.cadenaId = localStorage.getItem('cadena');
    this.getCatalogoData();
    this.selectedLocal = new Local();
  }
  confirmar(): void {
    this.showDetail = true;
  }
  create(): void {
    const result: OperationResult = new OperationResult(false, '');
    this.evaluationService.create(this.newEvaluation).subscribe(
      res => {
        result.Succeeded = res.Succeeded;
        result.Message = res.Message;
        result.Value = res.ReturnValue;

      },
      error => console.error('Error: ' + error),
      () => {
        if (result.Succeeded) {
          this.notificationService.printSuccessMessage(result.Message);
          this.router.navigate(['evaluaciones', result.Value.Slug, 'show']);
        } else {
          this.loading = true;

          this.notificationService.printErrorMessage(result.Message);
        }
      }
    );
  }

  getCatalogoData(): void {

    this.catalogService.set(this.apiEndpoint.catalogo() + this.apiEndpoint.posicionesByCadena() + this.cadenaId);
    this.catalogService.simpleGet().subscribe(
        res => {
            const data: any = res;
            this.posiciones = data.items;
        });

    this.catalogService.set(this.apiEndpoint.catalogo() + this.apiEndpoint.tiposVisita());
    this.catalogService.simpleGet().subscribe(
        res => {
            const data: any = res;
            this.tiposVisita = data.items;
        });

    this.catalogService.set(this.apiEndpoint.catalogo() + this.apiEndpoint.partesDia());
    this.catalogService.simpleGet().subscribe(
        res => {
            const data: any = res;
            this.partesDia = data.items;
        });

    this.evaluationService.getTipos().then(
        res => {
          const data: any = res;
          this.tiposEvaluacion = data;
        }
    );
  }

  onLocalSelected(localId: any): void {
    this.codigolocal = localId;
    this.onCodigoLocalBlur();
  }

  onCodigoLocalBlur(): void {
    const code: any = this.codigolocal;
    this.localService.getByCode(code)
    .then(res => {
      const data: any = res;
      this.selectedLocal = data;
    });
  }

  openSelector(): void {
    this.showSelector = true;
  }

  setTipoEvaluacion(event: any, tipoId: any, tipoNombre: string): void {
    const hasClass = event.target.classList.contains('on');
    if (hasClass) {
      event.target.classList.remove('on');

    } else {
      this.newEvaluation.TipoEvaluacion = tipoId;
      event.target.classList.add( 'on');
      this.notificationService.printSuccessMessage('Seleccionado ' + tipoNombre);
    }
  }

  createEvaluation(): void {
    this.loading = true;

    const result: OperationResult = new OperationResult(false, '');
    this.newEvaluation.IdLocal = this.selectedLocal.id;
    this.evaluationService.create(this.newEvaluation)
    .subscribe(res => {
      result.Succeeded = res.succeeded;
      result.Message = res.message;
      result.Value = res.returnValue;
      this.router.navigate(['evaluaciones']);

    },
    error => console.error('Error. ' + error),
    () => {
      if (result.Succeeded) {
        this.notificationService.printSuccessMessage(result.Message);
        this.evaluationService.setData(this.cadenaId, true).subscribe(forceRes => {
          this.notificationService.printSuccessMessage(forceRes);
          this.loading = false;

          this.router.navigate(['evaluaciones', result.Value.id, 'show']);
        });
      } else {

        this.notificationService.printErrorMessage(result.Message);
      }
    });
  }
}
