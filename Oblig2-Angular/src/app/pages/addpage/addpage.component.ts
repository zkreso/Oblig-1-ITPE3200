import { Component, OnInit } from '@angular/core';
import { DiseaseEntity, DiseaseSymptom } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { PageoptionsService } from '../../services/pageoptions.service';
import { map, Subject, BehaviorSubject, exhaustMap, withLatestFrom } from 'rxjs';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-addpage',
  templateUrl: './addpage.component.html',
  providers: [PageoptionsService, ErrorHandlingService]
})
export class AddpageComponent implements OnInit {

  // Variables for child component
  errorMessage$ = new BehaviorSubject<string | null>(null); // error message stream
  success: boolean = false;
  successString = "Disease created successfully";

  constructor(
    private ds: DatabaseService,
    private ps: PageoptionsService,
    private es: ErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  ngOndestroy(): void {
    this.subscription.unsubscribe();
  }

  // Stream of form data submissions
  private submit$ = new Subject<DiseaseEntity>();

  // React to form submission events by calling create method on server
  private subscription = this.submit$.pipe(
    withLatestFrom(this.ps.selectedSymptoms$),
    map( ([formData, latestSymptoms]): DiseaseEntity =>
      ({
        name: formData.name,
        description: formData.description,
        diseaseSymptoms: latestSymptoms.map( (symptom): DiseaseSymptom => ({symptomId: symptom.id}) )
      })
    ),
    exhaustMap(newDisease =>
      this.ds.createDisease(newDisease).pipe(
        this.es.handleErrors(this.errorMessage$, this.httpStatusToStrings)
      )
    )
  ).subscribe(() => {
    this.success = true;
  });

  private httpStatusToStrings(HttpStatusCode: number): string {
    switch (HttpStatusCode) {
      case 400: {
        return "Invalid input";
      }
      case 500: {
        return "Error occured on server. Please try again later";
      }
      default: {
        return "An unknown error occured";
      }
    }
  }

  createDisease(disease: DiseaseEntity) {
    this.success = false;
    this.submit$.next(disease);
  }

}
