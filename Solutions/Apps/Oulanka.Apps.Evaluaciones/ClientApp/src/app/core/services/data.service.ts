import { Response } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError, pipe, of } from 'rxjs';
import { map, retry, catchError} from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class DataService {

  public responseCache = new Map();
  public PageSize: number;
  public BaseUri: string;

  constructor( public http: HttpClient) { }

  set(baseUri: string, pageSize?: number): void {
    this.BaseUri = environment.APIEndPoint + baseUri;
    this.PageSize = pageSize;
  }

  simpleGet() {
    const uri = this.BaseUri;
    const fromCache = this.responseCache.get(uri);

    if (fromCache) {
      return fromCache;
    }

    const hResponse =  this.http.get(uri).pipe(map(response => (response as Response)));
    this.responseCache.set(uri, hResponse);
    return hResponse;
  }

  get(page: number) {
    const uri = this.BaseUri + page.toString() + '/' + this.PageSize.toString();
    const fromCache = this.responseCache.get(uri);

    if (fromCache) {
      return fromCache;
    }

    const hResponse =  this.http.get(uri).pipe(map(response => (response as Response)));
    this.responseCache.set(uri, hResponse);
    return hResponse;
  }

  getByParent(id: any, page: number) {
    const uri = this.BaseUri + id + '/' + page.toString() + '/' + this.PageSize.toString();
    const fromCache = this.responseCache.get(uri);
    if (fromCache) {
      return fromCache;
    }

    const hResponse =  this.http.get(uri).pipe(map(response => (response as Response)));
    this.responseCache.set(uri, hResponse);
    return hResponse;
  }


  getSingle(id: any) {
    const uri = this.BaseUri + '/' + id;
    const fromCache = this.responseCache.get(uri);
    if (fromCache) {
      return fromCache;
    }

    const hResponse =  this.http.get(uri).pipe(map(response => (response as Response)));
    this.responseCache.set(uri, hResponse);
    return hResponse;
  }

  post(data?: any, mapJson: boolean = true) {
    if (mapJson) {
      return this.http.post(this.BaseUri, data)
        .pipe(map(response => (response as Response) as any));
    } else {
      return this.http.post(this.BaseUri, data);
    }
  }

  delete(id: number) {
    return this.http.delete(this.BaseUri + '/' + id.toString())
      .pipe(map(response => (response as Response) as any));
  }

  deleteResource(resource: string) {
    return this.http.delete(resource)
      .pipe(map(response => (response as Response) as any));
  }
}
