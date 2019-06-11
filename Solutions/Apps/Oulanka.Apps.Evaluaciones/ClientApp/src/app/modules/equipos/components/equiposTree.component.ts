import {
  enableProdMode,
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild
} from '@angular/core';
import { EvaluationService } from '../../evaluations/services/evaluation.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { EquipoService } from '../services/equipo.service';
import { FlatTreeControl } from '@angular/cdk/tree';
import { OuFlatNode } from '../../../core/domain/ouFlatnode';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material';
import { OuNode } from '../../../core/domain/ouNode';
import { Observable, of as observableOf } from 'rxjs';

enableProdMode();
@Component({
  selector: 'app-equipostree',
  templateUrl: 'equiposTree.component.html'
})
export class EquiposTreeComponent implements OnInit {
  @Input() cadenaId: any;
  @Output() selected = new EventEmitter<any>();

  treeControl: FlatTreeControl<OuFlatNode>;
  treeFlattener: MatTreeFlattener<OuNode, OuFlatNode>;
  dataSource: MatTreeFlatDataSource<OuNode, OuFlatNode>;

  selectedId: any;

  public items: Array<OuNode>;
  public hasItems = false;

  constructor(
    public equipoService: EquipoService,
    public evaluationService: EvaluationService,
    public notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.getItems();

    this.treeFlattener = new MatTreeFlattener(
      this.transformer,
      this.getLevel,
      this.isExpandable,
      this.getChildren
    );
    this.treeControl = new FlatTreeControl<OuFlatNode>(
      this.getLevel,
      this.isExpandable
    );
    this.dataSource = new MatTreeFlatDataSource(
      this.treeControl,
      this.treeFlattener
    );
  }

  transformer = (node: OuNode, level: number) => {
    return new OuFlatNode(!!node.nodes, node.text, level, node.nodeType);
  }

  private getLevel = (node: OuFlatNode) => node.level;
  private isExpandable = (node: OuFlatNode) => node.expandable;
  private getChildren = (node: OuNode): Observable<OuNode[]> => observableOf(node.nodes);

  hasChild = (_: number, nodeData: OuFlatNode) => nodeData.expandable;

  getItems(): void {
    this.equipoService.getTree().then(res => {
      const data: any = res;
      this.items = data;
      this.dataSource.data = this.items;
      if (this.items !== undefined) {
        this.hasItems = true;
      }
    });
  }
}
