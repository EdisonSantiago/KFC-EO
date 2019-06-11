import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { DataService } from '../../core/services/data.service';
import { MembershipService } from './services/membership.service';
import { NotificationService } from '../../core/services/notification.service';

import { SharedModule } from '../../modules/shared/shared.module';

import { AccountComponent } from './components/account.component';
import { LoginComponent } from './components/login.component';
import { RegisterComponent } from './components/register.component';

import { accountRouting } from './routes';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    accountRouting
  ],
  declarations: [AccountComponent, LoginComponent, RegisterComponent],

  providers: [DataService, MembershipService, NotificationService]
})
export class AccountModule {}
