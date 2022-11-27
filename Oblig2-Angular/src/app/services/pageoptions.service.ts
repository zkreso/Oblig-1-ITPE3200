import { Injectable } from '@angular/core';
import { PageOptions, Symptom } from '../models';
import { BehaviorSubject, combineLatest, distinctUntilChanged, map, Observable } from 'rxjs';

@Injectable()
export class PageoptionsService {

  private selectedSymptomsSubject = new BehaviorSubject<Symptom[]>([]);

  private orderByOptions$ = new BehaviorSubject<string>("idAscending");
  private searchString$ = new BehaviorSubject<string>("");
  private pageNum$ = new BehaviorSubject<number>(1);
  private pageSize$ = new BehaviorSubject<number>(10);
  public selectedSymptoms$ = this.selectedSymptomsSubject as Observable<Symptom[]>;

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
    this.pageNum$.next(n);
  }

  nextPage(): void {
    let p = this.pageNum$.getValue() + 1;
    this.pageNum$.next(p);
  }

  prevPage(): void {
    let p = this.pageNum$.getValue() - 1;
    this.pageNum$.next(p);
  }

  setSearchString(s: string): void {
    this.searchString$.next(s);
    this.pageNum$.next(1);
  }

  addSymptom(symptom: Symptom): void {
    let symptoms = [...this.selectedSymptomsSubject.getValue(), symptom];
    this.selectedSymptomsSubject.next(symptoms);
  }

  removeSymptom(symptom: Symptom): void {
    let symptoms = this.selectedSymptomsSubject.getValue().filter(s => s.id != symptom.id);
    this.selectedSymptomsSubject.next(symptoms);
  }

  setAllSymptoms(symptoms: Symptom[]): void {
    this.selectedSymptoms = symptoms;
    this.selectedSymptomsSubject.next(symptoms);
  }

  sortById(): void {
    if (this.orderByOptions$.getValue() == "idAscending") {
      this.orderByOptions$.next("idDescending");
    } else {
      this.orderByOptions$.next("idAscending");
    }
  }

  sortByName(): void {
    if (this.orderByOptions$.getValue() == "nameAscending") {
      this.orderByOptions$.next("nameDescending");
    } else {
      this.orderByOptions$.next("nameAscending");
    }
  }

  changePageSize(n: number): void {
    this.pageSize$.next(n);
  }

}
