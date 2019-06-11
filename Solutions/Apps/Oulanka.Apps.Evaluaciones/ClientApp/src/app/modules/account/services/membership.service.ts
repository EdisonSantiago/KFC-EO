import { Injectable } from '@angular/core';
import { DataService } from '../../../core/services/data.service';
import { Registration } from '../domain/registration';
import { User } from '../domain/user';
import { UserView } from '../domain/userView';

@Injectable()
export class MembershipService {

    private accountRegisterAPI = 'api/account/register/';
    private accountLoginAPI = 'token';
    private accountLogoutAPI = 'api/account/logout/';

    constructor(public accountService: DataService) { }

    register(newUser: Registration) {

        this.accountService.set(this.accountRegisterAPI);
        return this.accountService.post(JSON.stringify(newUser));
    }

    login(creds: User) {
        this.accountService.set(this.accountLoginAPI);
        return this.accountService.post(JSON.stringify(creds));
    }

    logout() {
      /*  this.accountService.set(this.accountLogoutAPI);
        return this.accountService.post(null, false);*/
    }

    isUserAuthenticated(): boolean {
        const user: any = localStorage.getItem('userView');
        if (user != null) {
            return true;
        } else {
            return false;
        }
    }

    getLoggedInUser(): UserView {
        let userData: UserView;

        if (this.isUserAuthenticated()) {
            userData = JSON.parse(localStorage.getItem('userView'));
            userData = new UserView(userData.Id, userData.Username, userData.Email);
        }

        return userData;
    }
}
