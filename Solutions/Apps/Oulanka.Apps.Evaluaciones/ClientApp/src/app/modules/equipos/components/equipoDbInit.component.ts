import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { EquipoService } from '../services/equipo.service';

enableProdMode();
@Component({
  selector: 'app-equipo-dbinit',
  templateUrl: './equipoDbInit.component.html'
})
export class EquipoDbInitComponent implements OnInit {
  @Input() localId: any;
  @Input() evaluationId: any;

  constructor(
    public equipoService: EquipoService,
    public notificationService: NotificationService
  ) {}

  async ngOnInit(): Promise<void> {

    if (this.localId !== undefined) {
      await this.equipoService.setData(this.localId).subscribe(res => {
        if (res !== undefined) {
          this.notificationService.printSuccessMessage(res);
        }
      });
    }
  }
}
