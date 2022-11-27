import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../../services/database.service';
import { take, switchMap, BehaviorSubject, combineLatest } from 'rxjs';
import { ErrorHandlingService } from '../../services/error-handling.service';

const dict = new Map<number, string>([
  [500, "Error occured on server, please try again later"]
]);

@Component({
  selector: 'app-diseaselist',
  templateUrl: './diseaselist.component.html',
  providers: [ErrorHandlingService]
})
export class DiseaselistComponent implements OnInit {

  searchPlaceholder: string = "Enter disease name";
  searchLabel: string = "Search for diseases";

  searchString$ = new BehaviorSubject<string | undefined>("");
  deleteSuccess$ = new BehaviorSubject(false);
  deleteFailure$ = new BehaviorSubject("");

  diseases$ = combineLatest([
    this.deleteSuccess$,
    this.searchString$
  ]).pipe(
    switchMap(([_, searchString]) => this.ds.getAllDiseases(searchString).pipe(
      this.es.handleErrors(this.errorMessage$, dict)
    ))
  );

  public errorMessage$ = new BehaviorSubject<string | null>(null);

  constructor(private ds: DatabaseService, private es: ErrorHandlingService) { }

  ngOnInit(): void {
  }

  search(searchString: string) {
    this.searchString$.next(searchString);
  }

  clearSearch() {
    this.searchString$.next(undefined);
  }

  deleteDisease(id: number) {
    this.ds.deleteDisease(id).pipe(take(1)).subscribe(

      response => {
      console.log("Deleted successfully");
      this.deleteSuccess$.next(true);
      },

      error => {
        console.log(error);
        this.deleteFailure$.next("error");
      }
    );
  }
}
