import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { Cadena } from '../domain/cadena';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { OnlineOfflineService } from 'src/app/core/services/onlineOffline.service';
import { CadenaDatabase } from '../domain/cadenaDatabase';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CadenaService {
  private cadenaDb: any;

  constructor(
    public dataService: DataService,
    public apiEndPoint: ApiEndpointService,
    private readonly onlineOfflineService: OnlineOfflineService
  ) {
    this.cadenaDb = new CadenaDatabase();
  }

  public setData(): any {
    const setDataObservable = new Observable(observer => {
      this.cadenaDb.cadenas.count(count => {
        if (count <= 0 && this.onlineOfflineService.isOnline) {
          this.getItemsFromApi().subscribe(res => {
            const dbCadenas: Array<Cadena> = res.items;
            dbCadenas.forEach(async cadena => {
              if (
                (await this.cadenaDb.cadenas
                  .where('id')
                  .equals(cadena.id)
                  .count()) === 0
              ) {
                await this.cadenaDb.cadenas.add(cadena);
              } else {
                await this.cadenaDb.cadenas.update(cadena.id, cadena);
              }
            });

            observer.next('datos de cadenas sincronizados localmente');
            observer.complete();
          });
        }
      });
    });

    return setDataObservable;
  }

  // #region FROM API

  private getItemsFromApi() {
    this.dataService.set(this.apiEndPoint.cadenasList());
    return this.dataService.simpleGet();
  }

  // #endregion

  public getList() {
    return this.cadenaDb.cadenas.toArray();
  }

  public getBySlug(slug: string) {
    return this.cadenaDb.cadenas.where('id').equals(slug).first();
  }
}
