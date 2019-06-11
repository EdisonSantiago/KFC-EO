
import { Component, OnInit } from '@angular/core';

import { Cadena } from '../domain/cadena';
import { Paginated } from '../../../core/common/paginated';
import { DataService } from '../../../core/services/data.service';
import { UtilityService } from '../../../core/services/utility.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
    selector: 'app-cadenas',
    templateUrl: './cadenas.component.html'
})
export class CadenasComponent extends Paginated implements OnInit {
    private evaluationsAPI = 'api/cadenas/';
    public evaluations: Array<Cadena>;
    public isEmpty = true;

    constructor(public cadenaService: DataService,
                public utilityService: UtilityService,
                public notificationService: NotificationService) {
        super(0, 0, 0);
    }

    ngOnInit() {
        this.cadenaService.set(this.evaluationsAPI, 8);
        this.getProjects();
    }

    getProjects(): void {


        this.cadenaService.get(this.page)
            .subscribe(res => {
                const data: any = res.json();
                this.evaluations = data.Items;
                this.page = data.Page;
                this.pagesCount = data.TotalPages;
                this.totalCount = data.TotalCount;

                this.isEmpty = this.totalCount <= 0;

            },
            error => {
                if (error.status === 401 || error.status === 404) {
                    this.notificationService.printErrorMessage('Authentication required');
                    this.utilityService.navigateToSignIn();
                }
            });


    }

    search(page): void {
        super.search(page);
        this.getProjects();
    }
}
