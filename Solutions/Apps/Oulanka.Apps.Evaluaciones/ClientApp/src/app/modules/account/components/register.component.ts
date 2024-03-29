import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Registration } from '../domain/registration';
import { OperationResult } from '../../../core/domain/operationResult';
import { MembershipService } from '../services/membership.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
    selector: 'app-register',
    providers: [MembershipService, NotificationService],
    templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

    public newUser: Registration;

    constructor(public membershipService: MembershipService,
                public notificationService: NotificationService,
                public router: Router) { }

    ngOnInit() {
        this.newUser = new Registration('', '', '');
    }

    register(): void {
        const registrationResult: OperationResult = new OperationResult(false, '');
        this.membershipService.register(this.newUser)
            .subscribe(res => {
                registrationResult.Succeeded = res.Succeeded;
                registrationResult.Message = res.Message;

            },
            error => console.error('Error: ' + error),
            () => {
                if (registrationResult.Succeeded) {
                    this.notificationService.printSuccessMessage('Dear ' + this.newUser.Username + ', please login with your credentials');
                    this.router.navigate(['account/login']);
                } else {
                    this.notificationService.printErrorMessage(registrationResult.Message);
                }
            });
    }
}
