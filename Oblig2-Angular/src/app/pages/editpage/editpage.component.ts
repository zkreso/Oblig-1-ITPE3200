import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PageoptionsService } from '../../services/pageoptions.service';
import { DatabaseService } from '../../services/database.service';
import { ActivatedRoute } from '@angular/router';
import { Subject, map, exhaustMap, switchMap, forkJoin, take, withLatestFrom, BehaviorSubject } from 'rxjs';
import { DiseaseEntity, DiseaseSymptom } from '../../models';
import { ErrorHandlingService } from '../../services/error-handling.service';
import { DiseaseFormComponent } from '../../components/disease-form/disease-form.component';

const dict = new Map<number, string>([
  [400, "Invalid input"],
  [500, "Error occured on server, please try again later"]
]);

@Component({
  selector: 'app-editpage',
  templateUrl: './editpage.component.html',
  providers: [PageoptionsService, ErrorHandlingService]
})
export class EditpageComponent implements OnInit, AfterViewInit {

  @ViewChild(DiseaseFormComponent) child!: DiseaseFormComponent;

  // Variables for child component
  errorMessage$ = new BehaviorSubject<string | null>(null); // error message stream
  success: boolean = false;
  successString = "Disease edited successfully";

  constructor(
    private ds: DatabaseService,
    private ps: PageoptionsService,
    private es: ErrorHandlingService,
    private route: ActivatedRoute
  ) { }

  ngAfterViewInit() {
    // Fills form on first load
    this.route.paramMap.pipe(
      map(params => Number(params.get('id'))),
      switchMap(id => forkJoin([this.ds.getDisease(id), this.ds.getRelatedSymptoms(id)])),
      map(([disease, symptoms]) => ({ disease, symptoms })),
      take(1)
    ).subscribe(response => {
      this.child.form.patchValue(response.disease);
      this.ps.setAllSymptoms(response.symptoms);
      this.child.form.markAsUntouched();
    });
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  // Stream of form data submissions
  private submit$ = new Subject<DiseaseEntity>();

  // React to form submission events by calling update method on server
  private subscription = this.submit$.pipe(
    withLatestFrom(
      this.ps.selectedSymptoms$,
      this.route.paramMap.pipe(map(params => Number(params.get('id'))))
    ),
    map( ([formData, latestSymptoms, diseaseId]): DiseaseEntity => ({
      id: diseaseId,
      name: formData.name,
      description: formData.description,
      diseaseSymptoms: latestSymptoms.map((symptom): DiseaseSymptom => ({ symptomId: symptom.id, diseaseId: diseaseId }))
    })),
    exhaustMap(newDisease =>
      this.ds.updateDisease(newDisease).pipe(
        this.es.handleErrors(this.errorMessage$, dict)
      )
    )
  ).subscribe(() => {
    this.errorMessage$.next(null);
    this.success = true;
  });

  updateDisease(disease: DiseaseEntity) {
    this.success = false;
    this.submit$.next(disease);
  }

}
