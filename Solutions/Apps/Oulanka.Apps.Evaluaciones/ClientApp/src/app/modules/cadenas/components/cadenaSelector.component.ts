import { Component, OnInit } from '@angular/core';
import { Cadena } from '../domain/cadena';
import { DataService } from 'src/app/core/services/data.service';
import { UtilityService } from 'src/app/core/services/utility.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from 'src/app/core/services/notification.service';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { CatalogService } from 'src/app/core/services/catalog.service';
import { CadenaService } from '../services/cadena.service';

@Component({
  selector: 'app-cadenaselector',
  templateUrl: './cadenaselector.component.html'
})

export class CadenaSelectorComponent implements OnInit {
  public items: Array<any>;
  public isEmpty = true;
  public showSelector = true;
  public cadenaSelForm: FormGroup;
  private endPointAPI = 'api/cadenas/list';
  constructor(
    public formBuilder: FormBuilder,
    private itemService: DataService,
    private cadenaService: CadenaService,
    private utilityService: UtilityService,
    private router: Router,

    private catalogService: CatalogService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.cadenaSelForm = this.formBuilder.group({
      cadena: ['0', Validators.required]
    });

    this.cadenaService.setData().subscribe(res => {
    });

    this.itemService.set(this.endPointAPI);
    this.getItems();

    if (localStorage.getItem('cadena')) {
      this.showSelector = false;
    }
  }

  getItems(): void {
    this.itemService.simpleGet().subscribe(
      res => {
        const data: any = res;
        this.items = data.items;

        this.isEmpty = this.items.length <= 0;
      },
      error => {
        if (error.status === 401 || error.status === 404) {
          this.notificationService.printErrorMessage('Authentication required');
          this.utilityService.navigateToSignIn();
        }
      }
    );
  }

  setCadena(): void {
    localStorage.setItem('cadena', this.f.cadena.value);
    const cadenaId = this.f.cadena.value;
    let cadenaNombre: string;
    this.items.forEach(cadena => {
      if (cadena.id === cadenaId) {
        cadenaNombre = cadena.nombre;
        this.router.navigate(['evaluaciones']);

      }
    });

    localStorage.setItem('cadenaNombre', cadenaNombre);
    this.catalogService.setData(cadenaId).subscribe( res => {
      this.notificationService.printSuccessMessage(res);
      this.utilityService.navigateToNewEvaluation();
 

    });
  }

  get f() {
    return this.cadenaSelForm.controls;
  }
}
