import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  Location,
  LocationStrategy,
  HashLocationStrategy
} from '@angular/common';
import { Headers, RequestOptions, BaseRequestOptions } from '@angular/http';

import { AccountModule } from './modules/account/account.module';
import { SharedModule } from './modules/shared/shared.module';

import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HomeComponent } from './components/home.component';

import { appRoutingModule } from './app-routing.module';

import { DataService } from './core/services/data.service';
import { MembershipService } from './modules/account/services/membership.service';
import { UtilityService } from './core/services/utility.service';
import { NotificationService } from './core/services/notification.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './core/helpers/jwt.interceptor';
import { ErrorInterceptor } from './core/helpers/error.interceptor';
import { EvaluationModule } from './modules/evaluations/evaluation.module';
import { CadenaModule } from './modules/cadenas/cadena.module';
import { ApiEndpointService } from './core/services/apiEndpoint.service';
import { LocalModule } from './modules/locals/local.module';
import { StandardModule } from './modules/standards/standard.module';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { EquipoModule } from './modules/equipos/equipo.module';
import { ImagenesModule } from './modules/imagenes/imagenes.module';
import { LoaderInterceptor } from './core/helpers/loader.interceptor';
import { CatalogService } from './core/services/catalog.service';
import { MatTableModule } from '@angular/material';

@NgModule({
  declarations: [AppComponent, DashboardComponent, HomeComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    appRoutingModule,
    AccountModule,
    EvaluationModule,
    CadenaModule,
    LocalModule,
    StandardModule,
    EquipoModule,
    ImagenesModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ServiceWorkerModule.register('/main-sw.js', { enabled: environment.production })
  ],
  providers: [
    DataService,
    MembershipService,
    UtilityService,
    CatalogService,
    NotificationService,
    ApiEndpointService,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
