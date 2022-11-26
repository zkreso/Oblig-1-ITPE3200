import { Injectable } from '@angular/core';
import { PageOptions, Symptom } from '../models';
import { BehaviorSubject, combineLatest, distinctUntilChanged, map, Observable, shareReplay, Subject } from 'rxjs';

@Injectable()
export class PageoptionsService {

  private orderByOptionsSubject = new BehaviorSubject<string>("idAscending");
  private searchStringSubject = new BehaviorSubject<string>("");
  private pageNumberSubject = new BehaviorSubject<number>(1);
  private pageSizeSubject = new BehaviorSubject<number>(10);
  private selectedSymptomsSubject = new BehaviorSubject<Symptom[]>([]);

  private orderByOptions$ = this.orderByOptionsSubject as Observable<string>;
  private searchString$ = this.searchStringSubject as Observable<string>;
  private pageNum$ = this.pageNumberSubject as Observable<number>;
  private pageSize$ = this.pageSizeSubject as Observable<number>;
  selectedSymptoms$ = (this.selectedSymptomsSubject as Observable<Symptom[]>);

  pageOptions$: Observable<PageOptions> = combineLatest([
    this.orderByOptions$,
    this.searchString$.pipe(distinctUntilChanged()),
    this.pageNum$,
    this.pageSize$,
    this.selectedSymptoms$,
  ]).pipe(
    map(([orderByOptions, searchString, pageNum, pageSize, selectedSymptoms]) => ({
      orderByOptions,
      searchString,
      pageNum,
      pageSize,
      selectedSymptoms
    }))
  );

  private selectedSymptoms: Symptom[] = [];

  constructor() { }

  setPageNum(n: number): void {
    this.pageNumberSubject.next(n);
  }

  nextPage(): void {
    let p = this.pageNumberSubject.getValue() + 1;
    this.pageNumberSubject.next(p);
  }

  prevPage(): void {
    let p = this.pageNumberSubject.getValue() - 1;
    this.pageNumberSubject.next(p);
  }

  setSearchString(s: string): void {
    this.searchStringSubject.next(s);
    this.pageNumberSubject.next(1);
  }

  addSymptom(symptom: Symptom): void {
    this.selectedSymptoms.push(symptom);
    this.selectedSymptomsSubject.next(this.selectedSymptoms);
  }

  removeSymptom(symptom: Symptom): void {
    this.selectedSymptoms = this.selectedSymptoms.filter(s => s.id != symptom.id);
    this.selectedSymptomsSubject.next(this.selectedSymptoms);
  }

  setAllSymptoms(symptoms: Symptom[]): void {
    this.selectedSymptoms = symptoms;
    this.selectedSymptomsSubject.next(symptoms);
  }

  sortById(): void {
    if (this.orderByOptionsSubject.getValue() == "idAscending") {
      this.orderByOptionsSubject.next("idDescending");
    } else {
      this.orderByOptionsSubject.next("idAscending");
    }
  }

  sortByName(): void {
    if (this.orderByOptionsSubject.getValue() == "nameAscending") {
      this.orderByOptionsSubject.next("nameDescending");
    } else {
      this.orderByOptionsSubject.next("nameAscending");
    }
  }

  changePageSize(n: number): void {
    this.pageSizeSubject.next(n);
  }

}
