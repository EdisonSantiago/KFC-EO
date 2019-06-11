import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { environment } from 'src/environments/environment';
import { ApiEndpointService } from '../../../core/services/apiEndpoint.service';
import { ImageDatabase } from '../domain/ImageDatabase';
import { Observable } from 'rxjs';
import { ImagenLocal } from '../domain/imagenLocal';
import { OnlineOfflineService } from 'src/app/core/services/onlineOffline.service';

@Injectable({ providedIn: 'root' })
export class ImagenesService {
  private imageDb: any;

  constructor(
    public dataService: DataService,
    public apiEndpoint: ApiEndpointService,
    private readonly onlineOfflineService: OnlineOfflineService
  ) {
    this.imageDb = new ImageDatabase();
  }

  public setDataByLocal(localId: any): any {
    const setDataObservable = new Observable(observer => {
      this.imageDb.locales
        .where('localId')
        .equals(localId)
        .count(count => {
          if (count <= 0) {
            this.getByLocalFromApi(localId, 1).subscribe(res => {
              const images: Array<ImagenLocal> = res.items;
              images.forEach(async image => {
                if (
                  (await this.imageDb.locales
                    .where('id')
                    .equals(image.id)
                    .count()) === 0
                ) {
                  await this.imageDb.locales.add(image);
                } else {
                  await this.imageDb.locales.update(image.id, image);
                }
              });

              observer.next('imagenes Ok');
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

  // #region FROM / TO API

  private getByLocalFromApi(localId: any, page: number) {
    const apiUrl = this.apiEndpoint.imagenesByLocal() + localId + '/';
    this.dataService.set(apiUrl, environment.bigGridPageSize);
    return this.dataService.get(page);
  }

  private saveLocalImageToApi(image: ImagenLocal) {
    this.dataService.set(this.apiEndpoint.imagenesByLocalSave());
    return this.dataService.post(JSON.stringify(image));
  }
  // #endregion

  public getByEvaluacion(evaluacionId: any, page: number) {
    const apiUrl = this.apiEndpoint.imagenesByEvaluacion() + evaluacionId + '/';
    this.dataService.set(apiUrl, environment.gridPageSize);
    return this.dataService.get(page);
  }

  getByLocal(localId: any) {
    // return this.imageDb.locales.where('localId').equals(localId).toArray();
    //console.log(localId);
    return this.imageDb.locales
      .where('localId')
      .equals(localId)
      .toArray();
  }

  saveLocalImage(image: ImagenLocal) {
    return this.imageDb.locales.add(image);
  }

  async saveToApi(localId: any) {
    if (this.onlineOfflineService.isOnline) {
      const images: Array<ImagenLocal> = await this.imageDb.locales
        .where('localId')
        .equals(localId)
        .toArray();

      images.forEach((image: ImagenLocal) => {
        this.saveLocalImageToApi(image).subscribe( res => {
         // console.log(res);
        });
      });
    }
  }
}
