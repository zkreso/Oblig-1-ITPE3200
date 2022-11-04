import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiseasedetailsComponent } from './diseasedetails.component';

describe('DiseasedetailsComponent', () => {
  let component: DiseasedetailsComponent;
  let fixture: ComponentFixture<DiseasedetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiseasedetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiseasedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
