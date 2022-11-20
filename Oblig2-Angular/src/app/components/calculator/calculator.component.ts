import { Component, OnInit } from '@angular/core';
import { filter, map, Observable, of, concatMap, concatWith } from 'rxjs';
import { Disease } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { PageoptionsService } from '../../services/pageoptions.service';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html'
})
export class CalculatorComponent implements OnInit {

  public anyselected$: Observable<boolean> = this.ps.selectedSymptoms$.pipe(
    map(symptoms => symptoms.length > 0)
  );

  public diseases$: Observable<Disease[] | null> = this.ps.selectedSymptoms$.pipe(
    filter(symptoms => symptoms.length > 0),
    concatMap(symptoms => of(null).pipe(concatWith(this.ds.searchDisease(symptoms))))
  );

  constructor(private ps: PageoptionsService, private ds: DatabaseService) {
  }

  ngOnInit(): void { }
  
}
