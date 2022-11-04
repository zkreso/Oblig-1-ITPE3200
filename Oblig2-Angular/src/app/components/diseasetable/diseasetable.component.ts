import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-diseasetable',
  templateUrl: './diseasetable.component.html',
  styleUrls: ['./diseasetable.component.css']
})
export class DiseasetableComponent implements OnInit {
  @Input() vm: any;
  @Output() clickDeleteDisease: EventEmitter<number> = new EventEmitter;

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
