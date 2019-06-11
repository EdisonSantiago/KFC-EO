import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

import { NotificationService } from 'src/app/core/services/notification.service';

enableProdMode();
@Component({
  selector: 'app-standard-dbinit',
  templateUrl: './standardDbInit.component.html'
})
export class StandardDbInitComponent implements OnInit {
  @Input() evaluationId: any;
  @Input() tipoLocalId: any;

  constructor(
    public standardService: EstandardService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {
    if (this.evaluationId !== undefined && this.tipoLocalId !== undefined) {
      await this.standardService.setData().subscribe(res => {
        if (res !== undefined) {
          this.notificationService.printSuccessMessage(res);
        }
      });
    }
  }
}
