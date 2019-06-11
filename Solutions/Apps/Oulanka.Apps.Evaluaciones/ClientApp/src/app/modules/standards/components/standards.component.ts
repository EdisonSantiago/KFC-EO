import { enableProdMode, Component, OnInit, Input } from '@angular/core';
import { EstandardService } from '../services/standard.service';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

import { isUndefined } from 'util';
import { Standard } from '../domain/standard';

enableProdMode();
@Component({
  selector: 'app-standards',
  templateUrl: 'standards.component.html'
})
export class StandarsComponent implements OnInit {
  @Input() evaluationId: any;
  @Input() groupId: any;

  public hasItems = false;
  public items: Array<Standard>;

  constructor(
    public standardService: EstandardService,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    if (this.groupId !== undefined) {
      this.getItems(this.groupId);
    }
  }

  getItems(groupId: any): void {
    this.standardService
      .getItems(groupId)
      .then((dbStandards: Array<Standard>) => {
        const data: any = dbStandards;
        this.items = data;
        if (this.items !== undefined) {
          this.hasItems = true;

        }
      });
  }
}
