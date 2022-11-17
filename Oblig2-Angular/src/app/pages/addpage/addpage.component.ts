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
  styleUrls: ['./addpage.component.css'],
  providers: [PageoptionsService, ErrorHandlingService]
})
export class AddpageComponent implements OnInit {

  form = this.fb.group({
    name: ['', Validators.required],
    description: ''
  });

  private submit$ = new Subject<DiseaseEntity>();

  private subscription = this.submit$.pipe(
    withLatestFrom(this.ps.selectedSymptoms$),
    map( ([formdata, latestSymptoms]): DiseaseEntity =>
      ({
        name: formdata.name,
        description: formdata.description,
        diseaseSymptoms: latestSymptoms.map( (symptom): DiseaseSymptom => ({symptomId: symptom.id}) )
      })
    ),
    exhaustMap(
      newDisease => this.ds.createDisease(newDisease).pipe(
        catchError(this.es.handleError())
      )
    )
  ).subscribe(() => {
    this.success = true;
  });

  public errorMessage$ = this.es.notification$.pipe(
    map(httpStatusCode => {
      if (httpStatusCode != null) {
        return this.errorMessages(httpStatusCode);
      }
      return null;
    })
  );

  public success: null | boolean = null;

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
      case 401: {
        return "You need to log in to perform this action";
      }
      case 403: {
        return "You are not authorized to perform this action"
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
