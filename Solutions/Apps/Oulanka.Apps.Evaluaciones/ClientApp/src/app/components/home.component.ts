import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { UtilityService } from '../core/services/utility.service';
import { MembershipService } from '../modules/account/services/membership.service';
import { NotificationService } from '../core/services/notification.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

    constructor(public membershipService: MembershipService,
                public notificationService: NotificationService,
                public utilityService: UtilityService,
                public location: Location) { }

    ngOnInit() {
        if (this.isUserLoggedIn()) {
            this.utilityService.navigate('dashboard');
        } else {
            this.utilityService.navigateToSignIn();
        }
    }

    isUserLoggedIn(): boolean {
        return this.membershipService.isUserAuthenticated();
    }
}
