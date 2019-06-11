import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Cadena } from '../domain/cadena';
import { CadenaService } from '../services/cadena.service';
import { NotificationService } from '../../../core/services/notification.service';
import { UtilityService } from '../../../core/services/utility.service';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
    selector: 'app-cadenadetail',
    providers: [CadenaService, NotificationService],
    templateUrl: './cadenaDetail.component.html'
})
export class CadenaDetailComponent implements OnInit {

    private cadena: Cadena;
    private cadenaSlug: string;
    private sub: Subscription;

    constructor(public cadenaService: CadenaService,
                public notificationService: NotificationService,
                private route: ActivatedRoute,
                private router: Router) { }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.cadenaSlug = params.slug;
            this.getProject();
        });
    }

    getProject(): void {
        this.cadenaService.getBySlug(this.cadenaSlug)
            .subscribe(res => {
                const data: any = res.json();
                this.cadena = data;
            }, error => {
                this.notificationService.printErrorMessage(error);
            });
    }

}
