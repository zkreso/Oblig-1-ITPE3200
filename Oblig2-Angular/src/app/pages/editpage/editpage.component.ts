import { Component, OnInit } from '@angular/core';
import { PageoptionsService } from '../../services/pageoptions.service';
import { DatabaseService } from '../../services/database.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { Subject, map, exhaustMap, Observable, switchMap, forkJoin, take, combineLatest, catchError } from 'rxjs';
import { DiseaseEntity, Disease, DiseaseSymptom } from '../../models';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-editpage',
  templateUrl: './editpage.component.html',
  styleUrls: ['./editpage.component.css'],
  providers: [PageoptionsService]
})
export class EditpageComponent implements OnInit {

  disease$!: Observable<Disease>;

  form = this.fb.group({
    name: ['', Validators.required],
    description: ''
  });

  // TODO: Add validator for letters etc.

  private submit$ = new Subject<DiseaseEntity>();

  private subscription = combineLatest([
    this.route.paramMap.pipe(map(params => Number(params.get('id')))),
    this.submit$,
    this.ps.selectedSymptoms$
  ]).pipe(
    map(([diseaseId, formData, latestSymptoms]) => ({
      id: diseaseId,
      name: formData.name,
      description: formData.description,
      diseaseSymptoms: latestSymptoms.map(symptom => ({ symptomId: symptom.id, diseaseId: diseaseId}) as DiseaseSymptom)
      })
    ),
    exhaustMap(
      newDisease => this.ds.updateDisease(newDisease).pipe(
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
    private es: ErrorHandlingService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      map(params => Number(params.get('id'))),
      switchMap(id => forkJoin([this.ds.getDisease(id), this.ds.getRelatedSymptoms(id)])),
      map(([disease, symptoms]) => ({ disease, symptoms }),
      take(1)
      )).subscribe(data => {
        this.form.patchValue(data.disease);
        this.ps.setAllSymptoms(data.symptoms);
        this.form.markAsUntouched;
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  createDisease() {
    if (this.form.invalid) {
      return;
    }
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
