import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomstableComponent } from './symptomstable.component';

describe('SymptomstableComponent', () => {
  let component: SymptomstableComponent;
  let fixture: ComponentFixture<SymptomstableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SymptomstableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SymptomstableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
