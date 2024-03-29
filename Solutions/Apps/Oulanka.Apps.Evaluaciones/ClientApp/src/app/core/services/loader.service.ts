import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoaderState } from '../domain/loaderState';

@Injectable({ providedIn: 'root' })
export class LoaderService {
  private loaderSubject = new Subject<LoaderState>();
  loaderState = this.loaderSubject.asObservable();
  constructor() { }

  show() {
    this.loaderSubject.next({ show: true } as LoaderState);
  }

  hide() {
    this.loaderSubject.next({ show: false } as LoaderState);
  }
}
