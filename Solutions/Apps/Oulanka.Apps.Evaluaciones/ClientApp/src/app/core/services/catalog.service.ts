import { Injectable } from '@angular/core';
import { ApiEndpointService } from './apiEndpoint.service';
import { DataService } from './data.service';
import { CatalogDatabase } from '../domain/catalog/catalogDatabase';
import { Observable } from 'rxjs';
import { TipoVisita } from '../domain/catalog/tipoVisita';
import { ParteDia } from '../domain/catalog/parteDia';

@Injectable({ providedIn: 'root' })
export class CatalogService {
  private catalogDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService
  ) {
    this.catalogDb = new CatalogDatabase();
  }

  public setData(cadenaId: any): any {
    const setDataObservable = new Observable(observer => {
      // posiciones
      this.catalogDb.posiciones.count(count => {
        if (count <= 0) {
          this.getPositionsFromApi(cadenaId).subscribe( res => {
            const positions: Array<Position> = res.items;
            positions.forEach(async position => {
              await this.catalogDb.posiciones.add(position);
            });
            observer.next('posiciones OK');
          });
        }
      });

      // tiposVisita
      this.catalogDb.tiposVisita.count(count => {
        if (count <= 0) {
          this.getTiposVisitaFromApi().subscribe( res => {
            const tiposVisita: Array<TipoVisita> = res.items;
            tiposVisita.forEach(async tipo => {
              await this.catalogDb.tiposVisita.add(tipo);
            });
            observer.next('tipos de Visita OK');
          });
        }
      });

      // partesDia
      this.catalogDb.partesDia.count(count => {
        if (count <= 0) {
          this.getPartesDiaFromApi().subscribe( res => {
            const partesDia: Array<ParteDia> = res.items;
            partesDia.forEach(async parteDia => {
              await this.catalogDb.partesDia.add(parteDia);
            });
            observer.next('partes del dia OK');
          });
        }
      });

    });

    return setDataObservable;
  }

  // #region GET FROM API

  private getPositionsFromApi(cadenaId: any) {
    this.dataService.set(
      this.apiEndpoint.catalogo() + this.apiEndpoint.posicionesByCadena() + cadenaId
    );
    return this.dataService.simpleGet();
  }

  private getTiposVisitaFromApi() {
    this.dataService.set(
      this.apiEndpoint.catalogo() + this.apiEndpoint.tiposVisita()
    );
    return this.dataService.simpleGet();
  }

  private getPartesDiaFromApi() {
    this.dataService.set(this.apiEndpoint.catalogo() + this.apiEndpoint.partesDia());
    return this.dataService.simpleGet();
  }

  public getPositions(cadena: any) {
    return this.catalogDb.posiciones.where('idCadena').equals(cadena).toArray();
  }

  public getTiposVisita() {
    return this.catalogDb.tiposVisita.toArray();
  }

  public getPartesDia() {
    return this.catalogDb.partesDia.toArray();
  }

  // #endregion
}
