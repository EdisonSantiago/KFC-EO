import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { EvaluationService } from '../services/evaluation.service';

enableProdMode();
@Component({
  selector: 'app-evaluation-dbinit',
  templateUrl: './evaluationDbInit.component.html'
})
export class EvaluationDbInitComponent implements OnInit {
  @Input() cadenaId: any;

  constructor(
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {
    if (this.cadenaId === undefined) {
      this.cadenaId = localStorage.getItem('cadena');
    }

    if (this.cadenaId !== undefined) {
      await this.evaluationService
        .setData(this.cadenaId, false)
        .subscribe(res => {
          if (res !== undefined) {
            this.notificationService.printSuccessMessage(res);
          }
        });
    }
  }
}
