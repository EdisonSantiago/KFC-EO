import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { RespuestaService } from '../services/respuesta.service';

enableProdMode();
@Component({
  selector: 'app-respuesta-dbinit',
  templateUrl: './respuestaDbInit.component.html'
})
export class RespuestaDbInitComponent implements OnInit {
  @Input() evaluationId: any;

  constructor(
    public respuestaService: RespuestaService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {

    if (this.evaluationId !== undefined) {
      await this.respuestaService.setData(this.evaluationId).subscribe(res => {
        if (res !== undefined) {
          this.notificationService.printSuccessMessage(res);
        }
      });
    }
  }
}
