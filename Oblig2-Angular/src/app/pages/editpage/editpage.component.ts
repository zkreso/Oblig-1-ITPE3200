import { Component, OnInit } from '@angular/core';
import { PageoptionsService } from '../../services/pageoptions.service';
import { DatabaseService } from '../../services/database.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { Subject, map, exhaustMap, switchMap, forkJoin, take, withLatestFrom, BehaviorSubject } from 'rxjs';
import { DiseaseEntity, DiseaseSymptom } from '../../models';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-editpage',
  templateUrl: './editpage.component.html',
  providers: [PageoptionsService, ErrorHandlingService]
})
export class EditpageComponent implements OnInit {

  constructor(
    private ds: DatabaseService,
    private ps: PageoptionsService,
    private es: ErrorHandlingService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Subscribes to route params to populate form values
    // when component is loaded.
    // take(1) stops subscription after first emission
    this.route.paramMap.pipe(
      map(params => Number(params.get('id'))),
      switchMap(id => forkJoin([this.ds.getDisease(id), this.ds.getRelatedSymptoms(id)])),
      map(([disease, symptoms]) => ({ disease, symptoms })),
      take(1)
    ).subscribe(response => {
      this.form.patchValue(response.disease);
      this.ps.setAllSymptoms(response.symptoms);
      this.form.markAsUntouched();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  form = this.fb.group({
    name: ['', [Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]{1,}")]],
    description: ['', [Validators.nullValidator, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]*")]]
  });

  private submit$ = new Subject<DiseaseEntity>(); // submission events observable
  public success: null | boolean = null; // submission success
  public errorMessage$ = new BehaviorSubject<string | null>(null); // Error messages on failure

  // Subscription that triggers when submission events observable emits
  private subscription = this.submit$.pipe(
    withLatestFrom(
      this.route.paramMap.pipe(map(params => Number(params.get('id')))),
      this.ps.selectedSymptoms$
    ),
    map(([formData, diseaseId, latestSymptoms]): DiseaseEntity => ({
      id: diseaseId,
      name: formData.name,
      description: formData.description,
      diseaseSymptoms: latestSymptoms.map((symptom): DiseaseSymptom => ({ symptomId: symptom.id, diseaseId: diseaseId }))
    })),
    exhaustMap(
    newDisease => this.ds.updateDisease(newDisease).pipe(
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
