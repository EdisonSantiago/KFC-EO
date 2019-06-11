import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, NavigationStart, NavigationEnd, Params, PRIMARY_OUTLET } from '@angular/router';
import {map, filter} from 'rxjs/operators';
import { pipe } from 'rxjs';
import { enableProdMode } from '@angular/core';

enableProdMode();
import { NotificationService } from '../../../core/services/notification.service';
import {  Evaluation } from '../domain/evaluation';
import {  EvaluationService } from '../services/evaluation.service';


@Component({
    selector: 'app-sidebar',
    providers: [EvaluationService],
    templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit {

    public evaluation: Evaluation;
    public evaluationId: string;
    public hasEvaluation = false;

    constructor(public evaluationService: EvaluationService,
                public notificationService: NotificationService,
                public location: Location,
                private route: ActivatedRoute,
                private router: Router) { }

    ngOnInit(): void {

        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event => {
                let currentRoute: ActivatedRoute = this.route.root;
                while (currentRoute.children[0] !== undefined) {
                    currentRoute = currentRoute.children[0];
                }

                currentRoute.params.subscribe(params => {
                    this.evaluationId = params.id;
                    if (this.evaluationId !== undefined) {
                        this.getEvaluation();
                    } else {
                        this.hasEvaluation = false;
                        this.evaluation = undefined;
                    }
                });

            });

    }

    getEvaluation(): void {
        this.evaluationService.getBySlug(this.evaluationId)
            .then(res => {
                const data: any = res;
                this.evaluation = data;
                this.hasEvaluation = this.evaluation !== undefined;
            }, error => {
                this.notificationService.printErrorMessage(error);
            });
    }

}
