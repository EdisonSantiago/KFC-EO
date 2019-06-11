import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ImagenesService } from '../services/imagenes.service';
import { Paginated } from 'src/app/core/common/paginated';

enableProdMode();
@Component({
  selector: 'app-evalimages',
  templateUrl: 'imagenesevaluation.component.html'
})
export class ImagenesEvaluationComponent extends Paginated implements OnInit {
  @Input() evaluationId: any;

  public items: Array<any>;
  public hasItems = false;

  constructor(
    public imagenesService: ImagenesService,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {
    super(1, 10, 0);
  }

  ngOnInit(): void {
    if (this.evaluationId !== undefined) {
      this.getItems(this.evaluationId);

    }

  }

  getItems(evaluationId: any): void {
    this.imagenesService.getByEvaluacion(evaluationId, this.page)
    .subscribe(res => {
      const data: any = res;
      this.items = data.items;
      this.page = data.page;
      this.pagesCount = data.totalPages;
      this.totalCount = data.totalCount;

      this.hasItems = this.totalCount > 0;
    });
  }
}
