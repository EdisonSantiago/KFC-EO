import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { ApiEndpointService } from '../../../core/services/apiEndpoint.service';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { LocalDatabase } from '../domain/localDatabase';
import { Local } from '../domain/local';
import { OnlineOfflineService } from 'src/app/core/services/onlineOffline.service';

@Injectable({ providedIn: 'root' })
export class LocalService {
  private localDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService,
    private readonly onlineOfflineService: OnlineOfflineService,
  ) {
    this.localDb = new LocalDatabase();
  }

  public setData(cadena: any): any {
    const setDataObservable = new Observable(observer => {
      this.localDb.locales
        .where('cadenaId')
        .equals(cadena)
        .count(count => {
          if (count <= 0 && this.onlineOfflineService.isOnline) {
            this.getListFromApi(cadena).subscribe(res => {
              const dbLocales: Array<Local> = res.items;
              dbLocales.forEach(async local => {
                if (
                  (await this.localDb.locales
                    .where('id')
                    .equals(local.id)
                    .count()) === 0
                ) {
                  await this.localDb.locales.add(local);
                } else {
                  await this.localDb.locales.update(local.id, local);
                }
              });
              observer.next('datos de locales sincronizados localmente');
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

  private getListFromApi(cadena: any) {
    const apiUrl = this.apiEndpoint.localesByCadenaList() + '/' + cadena;
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  // #endregion

  public get(cadena: string, page: number) {
    const apiUrl = this.apiEndpoint.locales() + '/' + cadena + '/';
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.get(page);
  }

  public getList(cadena: any) {
    return this.localDb.locales
      .where('cadenaId')
      .equals(cadena)
      .toArray();
  }

  public getById(localId: any) {
    return this.localDb.locales
      .where('id')
      .equals(localId)
      .first();
  }

  public getByCode(code: any) {
    return this.localDb.locales
      .where('codigo')
      .equals(code)
      .first();
  }
}
