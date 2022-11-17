import { Component, OnInit } from '@angular/core';
import { map, switchMap, share, delay, takeUntil, Observable, startWith, merge } from 'rxjs';
import { Symptom } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { PageoptionsService } from '../../services/pageoptions.service';

@Component({
  selector: 'app-symptomsubpage',
  templateUrl: './symptomsubpage.component.html',
  styleUrls: ['./symptomsubpage.component.css']
})
export class SymptomsubpageComponent implements OnInit {

  // DATA FOR SEARCH COMPONENT

  searchPlaceholder: string = "Enter symptom name";
  searchLabel: string = "Search for symptoms";
  

  // DATA FOR SELECTED SYMPTOMS COMPONENT

  public selectedSymptoms$: Observable<Symptom[]> = this.ps.selectedSymptoms$;


  // DATA FOR SYMPTOM TABLE COMPONENT

  private data$ = this.ps.pageOptions$.pipe(
    switchMap(pageOptions => this.ds.getSymptomsPage(pageOptions)),
    share()
  );

  // This is just to delay spinner by 300 ms in case it loads
  // faster than that (don't want "flickering" on fast loads)
  private loadingStart$: Observable<boolean> = this.ps.pageOptions$.pipe(
    delay(300),
    takeUntil(this.data$),
    map(() => true)
  );
  private loadingEnd$: Observable<boolean> = this.data$.pipe(map(() => false));

  public loading$ = this.ps.pageOptions$.pipe(
    switchMap(() => merge(this.loadingStart$, this.loadingEnd$)),
    startWith(true)
  ); // loading indicator

  public symptoms$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.symptomList),
    startWith(null)
  ); // table data

  public options$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.pageData)
  ); // page navigation data

  constructor(private ds: DatabaseService, private ps: PageoptionsService) {
  }

  ngOnInit(): void { }

  // All the functions from the child components

  addSymptom(symptom: Symptom): void {
    this.ps.addSymptom(symptom);
  }

  removeSymptom(symptom: Symptom): void {
    this.ps.removeSymptom(symptom);
  }

  search(value: string): void {
    this.ps.setSearchString(value);
  }

  clearSearch(): void {
    this.ps.setSearchString("");
  }

  prevPage(): void {
    this.ps.prevPage();
  }

  nextPage(): void {
    this.ps.nextPage();
  }

  goToPage(n: number): void {
    this.ps.setPageNum(n);
  }

  setPageSize(n: number): void {
    this.ps.changePageSize(n);
  }
}
