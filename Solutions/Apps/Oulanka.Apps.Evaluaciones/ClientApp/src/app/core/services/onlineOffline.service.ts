import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

declare const window: any;

@Injectable({ providedIn: 'root' })
export class OnlineOfflineService {
  private internatConnectionChanged = new Subject<boolean>();

  constructor() {
    window.addEventListener('online', () => console.log('online'));
    window.addEventListener('offline', () => console.log('offline'));
  }

  get connectionChanged() {
    return this.internatConnectionChanged.asObservable();
  }

  get isOnline() {
    return !!window.navigator.onLine;
  }

  private updateOnlineStatus() {
    this.internatConnectionChanged.next(window.navigator.onLine);
  }
}
