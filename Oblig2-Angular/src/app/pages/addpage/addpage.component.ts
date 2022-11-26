import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
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

  constructor(
    private ds: DatabaseService,
    private ps: PageoptionsService,
    private fb: FormBuilder,
    private es: ErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  ngOndestroy(): void {
    this.subscription.unsubscribe();
  }

  form = this.fb.group({
    name: ['', [Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]{1,}")]],
    description: ['', [Validators.nullValidator, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]*")]]
  });

  private submit$ = new Subject<DiseaseEntity>(); // submitted form data stream
  public errorMessage$ = new BehaviorSubject<string | null>(null); // error message stream
  public success: null | boolean = null; // disease added successfully, for displaying message

  // Subscribe to form submissions to call server on submit
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
    this.form.markAsUntouched();
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

  createDisease() {
    if (this.form.invalid) {
      return;
    }
    this.success = false;
    this.submit$.next({
      name: this.form.value.name!,
      description: this.form.value.description!
    });
  }

}
