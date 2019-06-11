import { Component, OnInit, enableProdMode, Input } from '@angular/core';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { EstandardService } from '../services/standard.service';

import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';
import { GrupoStandard } from '../domain/grupoStandard';

enableProdMode();
@Component({
  selector: 'app-standardgroup',
  templateUrl: './standardGroup.component.html'
})
export class StandardGroupComponent implements OnInit {
  @Input() evaluationId: any;
  @Input() tipoLocalId: any;

  public hasGroups = false;
  public groups: GrupoStandard[] = [];
  public selectedGroup: GrupoStandard;

  constructor(
    public standardService: EstandardService,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService,
) {}

  async ngOnInit(): Promise<void> {
    if (this.evaluationId !== undefined && this.tipoLocalId !== undefined) {
        this.getItems(this.tipoLocalId);
    }
  }

  getItems(tipoLocalId: any): void {
    this.standardService.getGrupos().then((dbGroups: GrupoStandard[]) => {
      this.groups = dbGroups;
      if (this.groups !== undefined) {
        this.selectedGroup = this.groups[0];
        this.hasGroups = true;
      } else {
        this.notificationService.printErrorMessage('No hay Grupos Disponibles');
      }
    });
  }
}
