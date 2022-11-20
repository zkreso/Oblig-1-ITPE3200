import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Symptom } from '../../models';

@Component({
  selector: 'app-symptomstable',
  templateUrl: './symptomstable.component.html',
  styleUrls: ['./symptomstable.component.css']
})
export class SymptomstableComponent implements OnInit {

  @Input() symptoms: Symptom[] | null = null;
  @Input() loading: boolean | null = false;
  @Input() sortOptions: String | null = "idAscending";
  @Input() error: String | null = null;
  @Output() clickAddSymptom: EventEmitter<Symptom> = new EventEmitter;
  @Output() clickSortById = new EventEmitter;
  @Output() clickSortByName = new EventEmitter;
  
  constructor( ) { }

  ngOnInit(): void { }

  addSymptom(symptom: Symptom): void {
    this.clickAddSymptom.emit(symptom);
  }

  sortById() {
    this.clickSortById.emit();
  }

  sortByName() {
    this.clickSortByName.emit();
  }

}
