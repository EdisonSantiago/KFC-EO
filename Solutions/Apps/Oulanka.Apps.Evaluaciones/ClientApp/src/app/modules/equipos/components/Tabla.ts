import { Component, Injectable } from '@angular/core';
import { Sort } from '@angular/material';
import { enableProdMode,  OnInit, Input } from '@angular/core';
import { NotificationService } from 'src/app/core/services/notification.service';
import { EquipoService } from '../services/equipo.service';
import { RespuestaService } from '../../evaluations/services/respuesta.service';
import { Respuesta } from '../../evaluations/domain/respuesta';
import { Equipolocal } from '../domain/equipolocal';


export interface Food {
  calories: String;
  carbs: number;
  fat: number;
  name: string;
  protein: number;
}
@Component({
  selector: 'tabla',
  templateUrl: 'Tabla.html',
})
export class Tabla implements OnInit{
  @Input() evaluationId: any;
  @Input() localId: any;
  @Input() standard: any;

  public showDetail = false;
  public respuesta: Respuesta;

  constructor(
    public equipoService: EquipoService,
    //public notificationService: NotificationService,


    public respuestaService: RespuestaService,
    public notificationService: NotificationService,

  ) { }

  dataSource: Food[] = [
    { name: 'KFC', calories: "Cámara de Refrigeración", fat: 6, carbs: 24, protein: 4 },
    { name: 'KFC', calories: "Refrigerador Vertical 1 puerta", fat: 9, carbs: 37, protein: 4 },
    { name: 'KFC', calories: "Mesa Refrigerada Ensamble", fat: 16, carbs: 24, protein: 6 },
    { name: 'KFC', calories: "Congelador Horizontal", fat: 4, carbs: 67, protein: 4 },
    { name: 'KFC', calories: "Estufa 2 H (COCINA)", fat: 16, carbs: 49, protein: 4 },
  ];
  displayedColumns: string[] = ['name', 'calories', 'fat', 'carbs', 'protein'];
  

  ngOnInit(): void {

    //this.getRespuesta(this.standardId, this.evaluationId);
    this.getItem(this.localId);

  }

  getRespuesta(standardId: any, evaluationId: any) {

    this.respuestaService.getRespuestaByStandard(standardId, evaluationId)
      .then(res => {
        this.respuesta = res;
      });
  }
  getItem(itemId: any): void {
    this.equipoService.getById(itemId)
      .then((res: Equipolocal) => {
        const data: any = res;
        this.equipoService = data;
      });
  }
  }
  


