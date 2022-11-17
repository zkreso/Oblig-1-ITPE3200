import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomsubpageComponent } from './symptomsubpage.component';

describe('SymptomsubpageComponent', () => {
  let component: SymptomsubpageComponent;
  let fixture: ComponentFixture<SymptomsubpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SymptomsubpageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SymptomsubpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
