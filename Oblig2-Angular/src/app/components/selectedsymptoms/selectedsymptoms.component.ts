import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Symptom } from '../../models';

@Component({
  selector: 'app-selectedsymptoms',
  templateUrl: './selectedsymptoms.component.html',
  styleUrls: ['./selectedsymptoms.component.css']
})

export class SelectedsymptomsComponent implements OnInit {
  @Input() selectedSymptoms: Symptom[] | null = null;
  @Output() clickRemoveSymptom: EventEmitter<Symptom> = new EventEmitter;

  constructor() { }

  ngOnInit(): void { }

  removeSymptom(symptom: Symptom): void {
    this.clickRemoveSymptom.emit(symptom);
  }
}

