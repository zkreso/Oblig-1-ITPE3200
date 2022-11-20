import { Component, OnInit } from '@angular/core';
import { map, switchMap, share, delay, takeUntil, Observable, startWith, merge, catchError } from 'rxjs';
import { Symptom } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { ErrorHandlingService } from '../../services/error-handling.service';
import { PageoptionsService } from '../../services/pageoptions.service';

@Component({
  selector: 'app-symptomsubpage',
  templateUrl: './symptomsubpage.component.html',
  providers: [ErrorHandlingService]
})
export class SymptomsubpageComponent implements OnInit {

  constructor(
    private ds: DatabaseService,
    private ps: PageoptionsService,
    private es: ErrorHandlingService
  ) { }

  ngOnInit(): void { }

  // Set up calls to server whenever options change

  private data$ = this.ps.pageOptions$.pipe(
    switchMap(pageOptions => this.ds.getSymptomsPage(pageOptions).pipe(
        catchError(this.es.handleError())
      )
    ),
    share()
  );

  // 1.0 STATIC DATA FOR SEARCH COMPONENT

  searchPlaceholder: string = "Enter symptom name";
  searchLabel: string = "Search for symptoms";
  
  // 2.0 DYNAMIC DATA FOR SELECTED SYMPTOMS COMPONENT

  public selectedSymptoms$: Observable<Symptom[]> = this.ps.selectedSymptoms$;

  // 3.0 DYNAMIC DATA FOR SYMPTOM TABLE COMPONENT

  // 3.1 Current sort options for sort arrow indicator
  public sortOptions$ = this.data$.pipe(map(symptomsTable => symptomsTable.pageData.orderByOptions));

  // 3.2 Loading indicator, delayed to avoid "flickering"
  private loadingStart$: Observable<boolean> = this.ps.pageOptions$.pipe(
    delay(300),
    takeUntil(this.data$),
    map(() => true)
  );
  private loadingEnd$: Observable<boolean> = this.data$.pipe(map(() => false));

  public loading$ = this.ps.pageOptions$.pipe(
    switchMap(() => merge(this.loadingStart$, this.loadingEnd$)),
    startWith(true)
  );

  // 3.3 Symptoms for the table
  public symptoms$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.symptomList),
    startWith(null)
  );

  // 3.4 Error message if table fails to load
  public errorMessage$ = this.es.notification$.pipe(
    map(httpStatusCode => {
      if (httpStatusCode != null) {
        return this.errorMessages(httpStatusCode);
      }
      return null;
    })
  );

  errorMessages(HttpStatusCode: number): string {
    switch (HttpStatusCode) {
      case 500: {
        return "Error occured on server. Please try again later";
      }
      default: {
        return "An unknown error occured";
      }
    }
  }

  // 4.0 DYNAMIC DATA FOR PAGE NAVIGATION COMPONENT

  public options$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.pageData)
  );

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

  sortById() {
    this.ps.sortById();
  }

  sortByName() {
    this.ps.sortByName();
  }
}
