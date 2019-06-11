import { enableProdMode, Input, Component, OnInit } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { Standard } from '../domain/standard';

enableProdMode();
@Component({
  selector: 'app-substandards',
  templateUrl: 'substandards.component.html'
})
export class SubstandarsComponent implements OnInit {
  @Input() evaluationId: any;
  @Input() groupId: any;
  @Input() parentId: any;

  public hasItems = false;
  public items: Array<Standard>;
  public item: Standard;

  constructor(
    public standardService: EstandardService,
    private router: Router,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    if (this.parentId !== undefined) {
      this.getItem(this.parentId);
      this.getItems(this.parentId);
    }
  }

  getItem(itemId: any): void {
    this.standardService.getById(itemId)
    .then((res: Standard) => {
      const data: any = res;
      this.item = data;
    });
  }

  getItems(parentId: any): void {
    this.standardService.getsubItems(parentId)
    .then((subItems: Array<Standard>) => {
      const data: any = subItems;
      this.items = data;
      if (this.items !== undefined) {
        this.hasItems = true;
      }
    });
  }
}
