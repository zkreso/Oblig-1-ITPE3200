import { Component, OnInit } from '@angular/core';
import { map, switchMap, share, delay, takeUntil, Observable, startWith, merge, BehaviorSubject } from 'rxjs';
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

  // Listen to changes in options and call server to get new data
  private data$ = this.ps.pageOptions$.pipe(
    switchMap(pageOptions => this.ds.getSymptomsPage(pageOptions).pipe(
        this.es.handleErrors(this.errorMessage$, this.httpStatusToStrings)
      )
    ),
    share()
  );

  // Most of the code below is just the data stream above broken down so it can
  // be passed down to child components

  // Just some static labels for search bar child component
  searchPlaceholder: string = "Enter symptom name";
  searchLabel: string = "Search for symptoms";
  
  // Stream of selected symptoms for child component
  public selectedSymptoms$: Observable<Symptom[]> = this.ps.selectedSymptoms$;

  // Currently selected sorting options for child component
  public sortOptions$ = this.data$.pipe(map(symptomsTable => symptomsTable.pageData.orderByOptions));

  // Symptoms for child component table
  public symptoms$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.symptomList),
    startWith(null)
  );

  // Loading indicator for child component table, when waiting for symptoms to load.
  // Delayed by 400 ms to avoid "flickering"
  private loadingStart$: Observable<boolean> = this.ps.pageOptions$.pipe(
    delay(400),
    takeUntil(this.data$),
    map(() => true)
  );
  private loadingEnd$: Observable<boolean> = this.data$.pipe(map(() => false));

  public loading$ = this.ps.pageOptions$.pipe(
    switchMap(() => merge(this.loadingStart$, this.loadingEnd$)),
    startWith(true)
  );

  // Error message to display instead of table in child component if it fails to load
  public errorMessage$ = new BehaviorSubject<string | null>(null);

  private httpStatusToStrings(HttpStatusCode: number): string {
    switch (HttpStatusCode) {
      case 500: {
        return "Error occured on server. Please try again later";
      }
      default: {
        return "An unknown error occured";
      }
    }
  }

  // Page number and total pages for child component to make page navigation
  public options$ = this.data$.pipe(
    map(symptomsTable => symptomsTable.pageData)
  );

  // Functions from all the child components
  // Forwarded to the page options service
  // Validated in child components
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
