import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Evaluation } from '../domain/evaluation';
import { EvaluationService } from '../services/evaluation.service';
import { NotificationService } from '../../../core/services/notification.service';
import { UtilityService } from '../../../core/services/utility.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { LocalService } from '../../locals/services/local.service';
import { DataService } from 'src/app/core/services/data.service';
import { ApiEndpointService } from 'src/app/core/services/apiEndpoint.service';
import { ImageLocalUploadComponent } from '../../imagenes/components/image-local-upload/image-local-upload.component';
import { ImagenesLocalComponent } from '../../imagenes/components/imageneslocal.component';

@Component({
    selector: 'app-evaldetail',
    providers: [EvaluationService, NotificationService],
    templateUrl: './evaluationImagenes.component.html'
})
export class EvaluationImagenesComponent implements OnInit {

    @ViewChild(ImagenesLocalComponent) imageList: ImagenesLocalComponent;

    public evaluation: Evaluation;
    public evaluationId: any;
    public local: any;
    public sub: Subscription;
    public cadenaId: any;

    constructor(
        public catalogService: DataService,
        public localService: LocalService,
        public evaluationService: EvaluationService,
        public notificationService: NotificationService,
        private apiEndpoint: ApiEndpointService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.evaluationId = params.id;
            this.cadenaId = localStorage.getItem('cadena');
            this.getItem();

        });
    }

    getItem(): void {
        this.evaluationService.getBySlug(this.evaluationId)
            .then(res => {
                const data: any = res;
                this.evaluation = data;
                if ( this.evaluation !== undefined) {
                    this.getLocal(this.evaluation.localId);
                }
            }, error => {
                this.notificationService.printErrorMessage(error);
            });
    }

    getLocal(localId: any): void {
        this.localService.getById(localId)
        .then(res => {
            const data: any = res;
            this.local = data;
        }, error => {
            this.notificationService.printErrorMessage(error);
        });
    }

    onUpload(uploaded) {
        this.imageList.refresh();
    }
}
