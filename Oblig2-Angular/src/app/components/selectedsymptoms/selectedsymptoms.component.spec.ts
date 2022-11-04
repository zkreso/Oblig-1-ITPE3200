import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedsymptomsComponent } from './selectedsymptoms.component';

describe('SelectedsymptomsComponent', () => {
  let component: SelectedsymptomsComponent;
  let fixture: ComponentFixture<SelectedsymptomsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectedsymptomsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectedsymptomsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
