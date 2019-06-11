import { Injectable } from '@angular/core';
import { DataService } from 'src/app/core/services/data.service';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { OnlineOfflineService } from 'src/app/core/services/onlineOffline.service';
import Dexie from 'dexie';
import { Respuesta } from '../domain/respuesta';
import { Guid } from 'guid-typescript';
import { NotificationService } from 'src/app/core/services/notification.service';
import { environment } from 'src/environments/environment';
import { RespuestaDatabase } from '../domain/respuestaDatabase';
import { Observable } from 'rxjs';
import { RespuestaComentario } from '../domain/respuestaComentario';

@Injectable({ providedIn: 'root' })
export class RespuestaService {
  private respuestaDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService,
    private readonly onlineOfflineService: OnlineOfflineService,
    private notificationService: NotificationService
  ) {
    this.registerToEvents(onlineOfflineService);
    this.respuestaDb = new RespuestaDatabase();

  }

  public setData(evaluationId: any): any {
    const setDataObservable = new Observable(observer => {
      this.respuestaDb.respuestas.where('evaluacionId').equals(evaluationId).count(count => {
        if (count <= 0 && this.onlineOfflineService.isOnline) {
          this.getRespuestasFromApi(evaluationId).subscribe(res => {
            const dbRespuestas: Array<Respuesta> = res;
            dbRespuestas.forEach(async respuesta => {
              if ((await this.respuestaDb.respuestas.where('id').equals(respuesta.id).count()) === 0) {
                await this.respuestaDb.respuestas.add(respuesta);
              } else {
                await this.respuestaDb.respuestas.update(respuesta.id, respuesta);
              }
            });

            this.getCommentsFromApi(evaluationId).subscribe(commentRes => {
              const dbComments: Array<RespuestaComentario> = commentRes;
              dbComments.forEach (async comment => {
                if ((await this.respuestaDb.comentarios.where('id').equals(comment.id).count()) === 0) {
                  await this.respuestaDb.comentarios.add(comment);
                } else {
                  await this.respuestaDb.comentarios.update(comment.id, comment);
                }
              });
            });

            observer.next('respuestas de evaluacion sincronizados localmente!');
            observer.complete();

          });
        } else {
          observer.next();
          observer.complete();
        }
      });
    });

    return setDataObservable;
  }

  // #region FROM API

  private getRespuestasFromApi(evaluationId: any) {
    const apiUrl = this.apiEndpoint.respuestasByEvaluacion() + evaluationId;
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  private getCommentsFromApi(evaluationId: any) {
    const apiUrl = this.apiEndpoint.respuestasComentariosByEvaluacion() + evaluationId;
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  // #endregion



  getRespuestas(evaluationId: any) {
    return this.respuestaDb.respuestas.where('evaluacionId').equals(evaluationId).toArray();
  }

  getRespuestaByStandard(standardId: any, evaluationId: any) {
    return this.respuestaDb.respuestas
      .get({estandarId: standardId, evaluacionId: evaluationId});
  }

  saveRespuesta(respuesta: Respuesta) {
    return this.respuestaDb.respuestas.update(respuesta.id, respuesta);
  }



  // Events Default
  private registerToEvents(onlineOfflineService: OnlineOfflineService) {
    onlineOfflineService.connectionChanged.subscribe(online => {
      if (online) {
        console.log('went online');
        console.log('sending all stored items to API');
        this.sendItemsFromIndexedDbToApi();
      } else {
        console.log('went offline, storing in indexdb');
      }
    });
  }


  private async sendItemsFromIndexedDbToApi() {
    const allItems: Respuesta[] = await this.respuestaDb.respuestas.toArray();
    allItems.forEach((item: Respuesta) => {
      this.dataService.set(this.apiEndpoint.respuestasSave());
      this.dataService.post(JSON.stringify(item)).subscribe(res => {
        console.log('saved item');
        this.respuestaDb.answers.delete(item.id).then(() => {});
      });
    });

    this.notificationService.printSuccessMessage(
      'Sincronización completa ' + allItems.length + ' items'
    );
  }
}
