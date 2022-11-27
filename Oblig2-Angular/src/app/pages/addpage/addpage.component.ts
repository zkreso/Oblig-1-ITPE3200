import { Component, OnInit } from '@angular/core';
import { DiseaseEntity, DiseaseSymptom } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { PageoptionsService } from '../../services/pageoptions.service';
import { map, Subject, BehaviorSubject, exhaustMap, withLatestFrom } from 'rxjs';
import { ErrorHandlingService } from '../../services/error-handling.service';

const dict = new Map<number, string>([
  [400, "Invalid input"],
  [500, "Error occured on server, please try again later"]
]);

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
        this.es.handleErrors(this.errorMessage$, dict)
      )
    )
  ).subscribe(() => {
    this.success = true;
  });

  createDisease(disease: DiseaseEntity) {
    this.success = false;
    this.submit$.next(disease);
  }

}
