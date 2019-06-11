import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import {
  Router,
  ActivatedRoute,
  NavigationEnd,
  Params,
  PRIMARY_OUTLET
} from '@angular/router';
import { map, filter } from 'rxjs/operators';

import { enableProdMode } from '@angular/core';

enableProdMode();
import { MembershipService } from '../../../../modules/account/services/membership.service';
import { User } from '../../../../modules/account/domain/user';

interface IBreadcrumb {
  label: string;
  params: Params;
  url: string;
}

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html'
})
export class BreadcrumbComponent implements OnInit {
  public thisPage: any;
  public breadcrumbs: IBreadcrumb[];

  constructor(
    public membershipService: MembershipService,
    public location: Location,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.breadcrumbs = [];
  }

  ngOnInit() {
    this.getCurrentRoute();
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(event => {
        // set breadcrumbs
        const root: ActivatedRoute = this.route.root;
        this.breadcrumbs = this.getBreadcrumbs(root);
      });
  }

  private getBreadcrumbs(
    route: ActivatedRoute,
    url: string = '',
    breadcrumbs: IBreadcrumb[] = []
  ): IBreadcrumb[] {
    const ROUTE_DATA_BREADCRUMB = 'name';
    const children: ActivatedRoute[] = route.children;
    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      if (child.outlet !== PRIMARY_OUTLET) {
        continue;
      }

      if (!child.snapshot.data.hasOwnProperty(ROUTE_DATA_BREADCRUMB)) {
        continue;
      }

      const routeUrl: string = child.snapshot.url
        .map(segment => segment.path)
        .join('/');
      url += '/' + routeUrl;

      const breadcrumb: IBreadcrumb = {
        label: child.snapshot.data[ROUTE_DATA_BREADCRUMB],
        params: child.snapshot.params,
        url
      };

      breadcrumbs.push(breadcrumb);

      return this.getBreadcrumbs(child, url, breadcrumbs);
    }
  }

  getCurrentRoute(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(event => {
        let currentRoute = this.route.root;
        while (currentRoute.children[0] !== undefined) {
          currentRoute = currentRoute.children[0];
        }
        this.thisPage = currentRoute.snapshot.data;
      });
  }
}
