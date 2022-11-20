import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-diseasetable',
  templateUrl: './diseasetable.component.html'
})
export class DiseasetableComponent implements OnInit {
  @Input() vm: any;
  @Output() clickDeleteDisease: EventEmitter<number> = new EventEmitter;
  @Input() error: String | null = null;

  constructor() { }

  ngOnInit(): void {
  }

  deleteDisease(id: number) {
    if (id == NaN || id == undefined || id == null) {
      return;
    }
    this.clickDeleteDisease.emit(id);
  }

}
