import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { map } from 'rxjs/operators';
import { enableProdMode } from '@angular/core';

enableProdMode();
import { MembershipService } from '../../../../modules/account/services/membership.service';
import { UtilityService } from '../../../../core/services/utility.service';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(
    public membershipService: MembershipService,
    public utilityService: UtilityService,
    public authenticationService: AuthenticationService,
    public location: Location
  ) {}

  ngOnInit() {}

  isUserLoggedIn(): boolean {
    return this.membershipService.isUserAuthenticated();
  }

  getUserName(): string {
    if (this.isUserLoggedIn()) {
      const user = this.membershipService.getLoggedInUser();
      return user.Username;
    } else {
      return 'Ingreso';
    }
  }

  getCadena(): string {
      let cadena: string;
      if (this.isUserLoggedIn()) {
          cadena = localStorage.getItem('cadenaNombre');
      }
      return cadena;
  }

  logout(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('cadena');
    localStorage.removeItem('cadenaNombre');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('expires_in');
    localStorage.removeItem('userView');
    localStorage.removeItem('username');
    this.utilityService.navigateToHome();
  }
}
