import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiseaselistComponent } from './diseaselist.component';

describe('DiseaselistComponent', () => {
  let component: DiseaselistComponent;
  let fixture: ComponentFixture<DiseaselistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiseaselistComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiseaselistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
