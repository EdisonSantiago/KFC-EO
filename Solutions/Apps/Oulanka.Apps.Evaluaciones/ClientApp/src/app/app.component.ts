import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { enableProdMode } from '@angular/core';

enableProdMode();
import { MembershipService } from './modules/account/services/membership.service';
import { User } from './modules/account/domain/user';
import { SwUpdate } from '@angular/service-worker';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  constructor(
    private swUpdate: SwUpdate,
    public membershipService: MembershipService,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    if (this.swUpdate.isEnabled) {
      this.swUpdate.available.subscribe(async () => {
        if ( confirm('Una nueva versión del sistema está disponible. Cargar la nueva versión?' )) {
          window.location.reload();
        }
      });
    }
  }

  isUserLoggedIn(): boolean {
    return this.membershipService.isUserAuthenticated();
  }

  getUserName(): string {
    if (this.isUserLoggedIn()) {
      const user = this.membershipService.getLoggedInUser();
      return user.Username;
    } else {
      return 'Account';
    }
  }

}
