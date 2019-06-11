import { enableProdMode, Component, OnInit, Input} from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ImagenesService } from '../services/imagenes.service';
import { Paginated } from 'src/app/core/common/paginated';
import { LocalService } from '../../locals/services/local.service';
import { ImagenLocal } from '../domain/imagenLocal';

enableProdMode();
@Component({
  selector: 'app-localimages',
  templateUrl: 'imageneslocal.component.html'
})
export class ImagenesLocalComponent implements OnInit {
  @Input() localId: any;

  public items: Array<ImagenLocal>;
  public hasItems = false;

  constructor(
    public imagenesService: ImagenesService,
    public localService: LocalService,
    public notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    if (this.localId !== undefined) {
      this.getItems(this.localId);
    }
  }

  getItems(localId: any): void {
    this.imagenesService.getByLocal(localId).then(res => {
      const data: any = res;
      this.items = data;
      if (this.items !== undefined) {
        this.hasItems = this.items.length > 0;
      }
    });
  }

  refresh(): void {
    this.getItems(this.localId);
  }
}
