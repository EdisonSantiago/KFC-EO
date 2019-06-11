import { Component, OnInit } from '@angular/core';

import { Evaluation } from '../domain/evaluation';
import { Paginated } from '../../../core/common/paginated';
import { UtilityService } from '../../../core/services/utility.service';
import { NotificationService } from '../../../core/services/notification.service';
import { EvaluationService } from '../services/evaluation.service';

@Component({
  selector: 'app-evaluations',
  templateUrl: './evaluations.component.html'
})
export class EvaluationsComponent extends Paginated implements OnInit {
  public items: Array<Evaluation>;
  public isEmpty = true;

  constructor(
    public dataService: EvaluationService,
    public utilityService: UtilityService,
    public notificationService: NotificationService
  ) {
    super(1, 10, 0);
  }

  ngOnInit() {
    this.getProjects();
  }

  getProjects(): void {
    const cadena = localStorage.getItem('cadena');
    this.dataService.get(cadena, this.page)
    .subscribe(
      res => {
        const data: any = res;
        this.items = data.items;
        this.page = data.page;
        this.pagesCount = data.totalPages;
        this.totalCount = data.totalCount;

        this.isEmpty = this.totalCount <= 0;
      },
      error => {
        if (error.status === 401 || error.status === 404) {
          this.notificationService.printErrorMessage('Authentication required');
          this.utilityService.navigateToSignIn();
        }
      }
    );
  }

  search(page): void {
    super.search(page);
    this.getProjects();
  }
}
