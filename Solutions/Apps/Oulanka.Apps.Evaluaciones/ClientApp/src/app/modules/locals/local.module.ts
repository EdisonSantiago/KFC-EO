import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UtilityService } from 'src/app/core/services/utility.service';
import { LocalService } from './services/local.service';
import { LocalSelectorComponent } from './components/localSelector.component';
import { LocalDbInitComponent } from './components/localDbInit.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule
    ],
    declarations: [
        LocalSelectorComponent,
        LocalDbInitComponent
    ],
    providers: [
        DataService,
        NotificationService,
        UtilityService,
        LocalService
    ],
    exports: [
        LocalSelectorComponent,
        LocalDbInitComponent
    ]
})
export class LocalModule { }
