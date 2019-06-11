import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Paginated } from '../../../core/common/paginated';
import { LocalService } from '../services/local.service';
import { UtilityService } from '../../../core/services/utility.service';
import { NotificationService } from '../../../core/services/notification.service';
import { getLocaleDateFormat, getLocaleNumberSymbol } from '@angular/common';

@Component({
  selector: 'app-localselector',
  providers: [LocalService],
  templateUrl: './localSelector.component.html'
})
export class LocalSelectorComponent extends Paginated implements OnInit {
  @Input() showSelector = false;
  @Input() cadenaId: any;
  @Output() selected = new EventEmitter<string>();

  public items: Array<any>;
  public hasItems = false;

  constructor(
    public localService: LocalService,
    public utilityService: UtilityService,
    public notificationService: NotificationService
  ) {
    super(1, 10, 0);
  }

  ngOnInit(): void {
    if (this.cadenaId !== undefined) {
      this.getItems(this.cadenaId);
    }
  }

  getItems(cadenaId: any): void {
    this.localService.get(cadenaId, this.page)
    .subscribe(
      res => {
        const data: any = res;
        this.items = data.items;
        this.page = data.page;
        this.pagesCount = data.totalPages;
        this.totalCount = data.totalCount;
        this.hasItems = this.totalCount > 0;
      }
    );
  }

  selectLocal(localId: string) {
    this.selected.emit(localId);
    this.showSelector = false;
  }

}
