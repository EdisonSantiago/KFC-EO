import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { environment } from 'src/environments/environment';
import { ApiEndpointService } from '../../../core/services/apiEndpoint.service';
import { EquipoDatabase } from '../domain/equipoDatabase';
import { Observable } from 'rxjs';
import { Equipolocal } from '../domain/equipolocal';

@Injectable({ providedIn: 'root' })
export class EquipoService {

  private equipoDb: any;
  private equipodb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService
  ) {
    this.equipoDb = new EquipoDatabase();
    this.equipodb = new EquipoDatabase();

  }

  public setData(localId: any): any {
    const setDataObservable = new Observable(observer => {
      this.equipoDb.tree.count(count => {
        if (count <= 0) {
          this.getTreeFromAPI().subscribe(res => {
            const nodes = res;
            nodes.forEach(async element => {
              if ((await this.equipoDb.tree.where('id').equals(element.id).count()) === 0) {
                await this.equipoDb.tree.add(element);
              } else {
                await this.equipoDb.tree.update(element.id, element);
              }
            });

            observer.next('imagenes ok!');
            observer.complete();
          });
        }
      });
    });

    return setDataObservable;
  }

// #region FROM API

public getTreeFromAPI() {
  const apiUrl = this.apiEndpoint.equiposTree();
  this.dataService.set(apiUrl, environment.gridPageSize);
  return this.dataService.simpleGet();
}
  public getById(itemId: any) {

    return this.equipoDb.tree.where('id').equals(itemId).first();

  }
  public getItems(grupoId: any) {

    return this.equipoDb.grupoStandards
      .where('idlocal')
      .equals(grupoId)
      .filter((item: Equipolocal) => {
        return item.cantidad;
      })
      .toArray();
  }
 
// #endregion

public getTree() {
  return this.equipoDb.tree.toArray();
}
}
