import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { EvaluationForm } from '../domain/evaluationForm';
import { Evaluation } from '../domain/evaluation';
import Dexie from 'dexie';
import { environment } from 'src/environments/environment';
import { ApiEndpointService } from '../../../core/services/apiEndpoint.service';
import { OnlineOfflineService } from 'src/app/core/services/onlineOffline.service';
import { EvaluationDatabase } from '../domain/evaluationDatabase';
import { Observable } from 'rxjs';
import { TipoEvaluationDatabase } from '../domain/tipoEvaluationDatabase';

@Injectable({ providedIn: 'root' })
export class EvaluationService {
  private getListAPI = 'api/evaluaciones';
  private evaluationDb: any;
  private tipoEvaluationDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService,
    private readonly onlineOfflineService: OnlineOfflineService
  ) {
    this.evaluationDb = new EvaluationDatabase();
    this.tipoEvaluationDb = new TipoEvaluationDatabase();
  }

  public setData(cadenaId: any, force: boolean): any {
    const setDataObservable = new Observable(observer => {
      this.tipoEvaluationDb.tipoEvaluations.count(tipoCount => {
        if (tipoCount <= 0 ) {
          this.getTiposFromApi().subscribe(tipoRes => {
            const tipos = tipoRes.items;
            tipos.forEach(async tipo => {
              if (
                (await this.tipoEvaluationDb.tipoEvaluations
                  .where('id')
                  .equals(tipo.id)
                  .count()) === 0
              ) {
                await this.tipoEvaluationDb.tipoEvaluations.add(tipo);
              } else {
                await this.tipoEvaluationDb.tipoEvaluations.update(tipo.id, tipo);
              }
            });
          });
        }
      });

      this.evaluationDb.evaluations.count(async count => {
        if (count <= 0 || force) {
          await this.evaluationDb.evaluations.clear();
          this.getListFromAPI(cadenaId, 1).subscribe(res => {
            const evaluations: Array<Evaluation> = res.items;
            evaluations.forEach(async item => {
              await this.evaluationDb.evaluations.add(item);
            });

            let message = 'sincronizado';
            if (force) {
              message += ' forzado';
            }
            observer.next(message + ' ok!');
            observer.complete();
          });
        } else {
          this.getListFromAPI(cadenaId, 1).subscribe(res => {
            const evaluations: Array<Evaluation> = res.items;
            evaluations.forEach(async item => {
              if (
                (await this.evaluationDb.evaluations
                  .where('id')
                  .equals(item.id)
                  .count()) === 0
              ) {
                await this.evaluationDb.evaluations.add(item);
              } else {
                await this.evaluationDb.evaluations.update(item.id, item);
              }
            });

            observer.next('sincronizado ok!');
            observer.complete();
          });
        }
      });
    });

    return setDataObservable;
  }

  private registerToEvents(onlineOfflineService: OnlineOfflineService) {
    onlineOfflineService.connectionChanged.subscribe(online => {
      if (online) {
        console.log('went online');
        console.log('sending all stored items to API');
      } else {
        console.log('went offline, storing in indexdb');
      }
    });
  }

  // #region GET FROM API

  public getListFromAPI(cadena: string, page: number) {
    const apiUrl = this.getListAPI + '/' + cadena + '/';
    this.dataService.set(apiUrl, environment.bigGridPageSize);
    return this.dataService.get(page);
  }

  public getTiposFromApi() {
    const apiUrl =
      this.apiEndpoint.evaluaciones() + this.apiEndpoint.tipoEvaluaciones();
    this.dataService.set(apiUrl);
    return this.dataService.simpleGet();
  }

  // #endregion

  create(newEvaluation: EvaluationForm) {
    this.dataService.set(this.apiEndpoint.evaluacionesCreate());
    return this.dataService.post(JSON.stringify(newEvaluation));
  }

  public getBySlug(slug: string) {
    return this.evaluationDb.evaluations.get(slug);
  }

  public get(cadena: string, page: number) {
    const apiUrl = this.getListAPI + '/' + cadena + '/';
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.get(page);
  }

  public getTipos() {
    return this.tipoEvaluationDb.tipoEvaluations.toArray();
  }
}
