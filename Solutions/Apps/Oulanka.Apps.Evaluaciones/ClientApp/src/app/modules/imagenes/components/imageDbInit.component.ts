import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ImagenesService } from '../services/imagenes.service';

enableProdMode();
@Component({
  selector: 'app-imagen-dbinit',
  templateUrl: './imageDbInit.component.html'
})
export class ImageDbInitComponent implements OnInit {
  @Input() localId: any;
  @Input() evaluationId: any;

  constructor(
    public imagenService: ImagenesService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {

    if (this.localId !== undefined) {
      await this.imagenService.setDataByLocal(this.localId).subscribe(res => {
        if (res !== undefined) {
          this.notificationService.printSuccessMessage(res);
        }
      });
    }
  }
}
