import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../../services/database.service';
import { take, switchMap, BehaviorSubject, combineLatest } from 'rxjs';

@Component({
  selector: 'app-diseaselist',
  templateUrl: './diseaselist.component.html',
  styleUrls: ['./diseaselist.component.css']
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
    switchMap(([_, searchString]) => this.ds.getAllDiseases(searchString))
  );

  constructor(private ds: DatabaseService) { }

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
