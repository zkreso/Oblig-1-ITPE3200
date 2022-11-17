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
  @Output() clickAddSymptom: EventEmitter<Symptom> = new EventEmitter;
  
  constructor() { }

  ngOnInit(): void { }

  addSymptom(symptom: Symptom): void {
    this.clickAddSymptom.emit(symptom);
  }

}
