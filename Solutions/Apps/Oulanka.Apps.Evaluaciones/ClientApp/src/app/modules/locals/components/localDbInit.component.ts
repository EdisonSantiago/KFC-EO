import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { LocalService } from '../services/local.service';

enableProdMode();
@Component({
  selector: 'app-local-dbinit',
  templateUrl: './localDbInit.component.html'
})
export class LocalDbInitComponent implements OnInit {
  @Input() cadenaId: any;

  constructor(
    public localService: LocalService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {
    if (this.cadenaId === undefined) {
      this.cadenaId = localStorage.getItem('cadena');
    }

    if (this.cadenaId !== undefined) {
      await this.localService.setData(this.cadenaId).subscribe(res => {
        if (res !== undefined) {
          this.notificationService.printSuccessMessage(res);
        }
      });
    }
  }
}
