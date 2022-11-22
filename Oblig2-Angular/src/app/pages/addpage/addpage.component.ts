import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { DiseaseEntity, DiseaseSymptom } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { PageoptionsService } from '../../services/pageoptions.service';
import { map, Subject, exhaustMap, withLatestFrom, catchError } from 'rxjs';
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

  private submit$ = new Subject<DiseaseEntity>(); // submission events observable
  public success: null | boolean = null; // submission success

  // Subscription that triggers when submission events observable emits
  private subscription = this.submit$.pipe(
    withLatestFrom(this.ps.selectedSymptoms$),
    map( ([formData, latestSymptoms]): DiseaseEntity =>
      ({
        name: formData.name,
        description: formData.description,
        diseaseSymptoms: latestSymptoms.map( (symptom): DiseaseSymptom => ({symptomId: symptom.id}) )
      })
    ),
    exhaustMap(
      newDisease => this.ds.createDisease(newDisease).pipe(
        catchError(this.es.handleError())
      )
    )
  ).subscribe(() => {
    this.form.markAsUntouched();
    this.success = true;
  });

  public errorMessage$ = this.es.notification$.pipe(
    map(httpStatusCode => {
      httpStatusCode
        ? this.errorMessages(httpStatusCode)
        : null
    })
  );

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

  errorMessages(HttpStatusCode: number): string {
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

}
