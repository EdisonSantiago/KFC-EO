import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DataService } from 'src/app/core/services/data.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UtilityService } from 'src/app/core/services/utility.service';
import { equiposRouting } from './routes';
import { EquipoService } from './services/equipo.service';
import { EquiposTreeComponent } from './components/equiposTree.component';
import { MatTreeModule, MatIconModule, MatButtonModule, MatTableModule } from '@angular/material';
import { EquipoDbInitComponent } from './components/equipoDbInit.component';
import { Tabla } from './components/tabla';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatTableModule,
    equiposRouting,
    MatTreeModule,
    MatIconModule,
    MatButtonModule
  ],
  declarations: [EquiposTreeComponent, EquipoDbInitComponent, Tabla],
  providers: [DataService, NotificationService, UtilityService, EquipoService],
  exports: [EquiposTreeComponent, EquipoDbInitComponent, Tabla]
})
export class EquipoModule {}
