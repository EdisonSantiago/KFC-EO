import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { environment } from 'src/environments/environment';
import { ApiEndpointService } from '../../../core/services/apiEndpoint.service';
import { StandardDatabase } from '../domain/standardDatabase';
import { Standard } from '../domain/standard';
import { GrupoStandard } from '../domain/grupoStandard';
import { TipoStandard } from '../domain/tipoStandard';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class EstandardService {
  private getListAPI = 'api/evaluaciones';
  private standardDb: any;
  private grupoDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService
  ) {
    this.standardDb = new StandardDatabase();
  }

  public setData(): any {
    const setDataObservable = new Observable(observer => {
      this.standardDb.grupoStandards.count(count => {
        if (count <= 0) {
          this.getGruposFromApi().subscribe(res => {
            const grupos: GrupoStandard[] = res;
            grupos.forEach(async grupo => {
              // get grupos & save to IDb
              if (
                (await this.standardDb.grupoStandards
                  .where('id')
                  .equals(grupo.id)
                  .count()) === 0
              ) {
                await this.standardDb.grupoStandards.add(grupo);
              } else {
                await this.standardDb.grupoStandards.update(grupo.id, grupo);
              }

              // get items & save to IDb
              this.getItemsFromApi(grupo.id).subscribe(itemRes => {
                const items: Standard[] = itemRes;
                items.forEach(async item => {
                  if (
                    (await this.standardDb.standards
                      .where('id')
                      .equals(item.id)
                      .count()) === 0
                  ) {
                    await this.standardDb.standards.add(item);
                  } else {
                    await this.standardDb.standards.update(item.id, item);
                  }

                  // get PickList & save to IDb
                  this.getPicklistFromApi(item.id).subscribe(pickRes => {
                    const pickItems: Standard[] = pickRes;
                    pickItems.forEach(async pickItem => {
                      if (
                        (await this.standardDb.standards
                          .where('id')
                          .equals(pickItem.id)
                          .count()) === 0
                      ) {
                        await this.standardDb.standards.add(pickItem);
                      } else {
                        await this.standardDb.standards.update(
                          pickItem.id,
                          pickItem
                        );
                      }
                    });
                  });

                  // get sub items & save to IDb
                  this.getsubItemsFromApi(item.id).subscribe(subRes => {
                    const subItems: Standard[] = subRes;
                    subItems.forEach(async subItem => {
                      if (
                        (await this.standardDb.standards
                          .where('id')
                          .equals(subItem.id)
                          .count()) === 0
                      ) {
                        await this.standardDb.standards.add(subItem);
                      } else {
                        await this.standardDb.standards.update(
                          subItem.id,
                          subItem
                        );
                      }

                      // get PickList & save to IDb
                      this.getPicklistFromApi(subItem.id).subscribe(pickRes => {
                        const pickItems: Standard[] = pickRes;
                        pickItems.forEach(async pickItem => {
                          if (
                            (await this.standardDb.standards
                              .where('id')
                              .equals(pickItem.id)
                              .count()) === 0
                          ) {
                            await this.standardDb.standards.add(pickItem);
                          } else {
                            await this.standardDb.standards.update(
                              pickItem.id,
                              pickItem
                            );
                          }
                        });

                        observer.next('estandares sincronizados ok!');
                        observer.complete();
                      });
                    });
                  });
                });
              });
            });
          });
        } else {
          observer.next();
          observer.complete();
        }
      });
    });
    return setDataObservable;
  }

  // #region API CALLS

  private getGruposFromApi() {
    const apiUrl = this.apiEndpoint.grupoEstandaresList();
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  private getItemsFromApi(grupoId: any) {
    const apiUrl = this.apiEndpoint.estandaresByGrupo() + grupoId + '/1';
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  private getsubItemsFromApi(parentId: any) {
    const apiUrl = this.apiEndpoint.estandaresByParent() + parentId;
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  private getPicklistFromApi(parentId: any) {
    const apiUrl = this.apiEndpoint.picklistByParent() + parentId;
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.simpleGet();
  }

  // #endregion

  public getById(itemId: any) {
    return this.standardDb.standards.where('id').equals(itemId).first();
  }

  public getGrupos() {
    return this.standardDb.grupoStandards.orderBy('nombre').toArray();
  }

  public getItems(grupoId: any) {
    return this.standardDb.standards
      .where('grupoEstandarId')
      .equals(grupoId)
      .filter((item: Standard) => {
        return item.tipoEstandar === TipoStandard.Contenedor;
      })
      .toArray();
  }

  public getsubItems(parentId: any) {
    return this.standardDb.standards
      .where('estandarPadreId')
      .equals(parentId)
      .filter((item: Standard) => {
        return item.tipoEstandar === TipoStandard.Estandar;
      })
      .toArray();
  }

  public getPicklist(parentId: any) {
    return this.standardDb.standards
      .where('estandarPadreId')
      .equals(parentId)
      .filter((item: Standard) => {
        return item.tipoEstandar === TipoStandard.Opcion;
      })
      .toArray();
  }
}
