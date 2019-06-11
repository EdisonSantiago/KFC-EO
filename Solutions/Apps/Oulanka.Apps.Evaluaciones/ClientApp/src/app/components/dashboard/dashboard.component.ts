
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { map } from 'rxjs/operators';
import { enableProdMode } from '@angular/core';
enableProdMode();

import { SharedModule } from '../../modules/shared/shared.module';

import { MembershipService } from '../../modules/account/services/membership.service';
import { User } from '../../modules/account/domain/user';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {

    constructor(public membershipService: MembershipService,
                public location: Location) { }

    ngOnInit() {
        // this.location.go('evaluaciones/new');
    }






}
