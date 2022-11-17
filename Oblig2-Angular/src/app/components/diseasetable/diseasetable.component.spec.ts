import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiseasetableComponent } from './diseasetable.component';

describe('DiseasetableComponent', () => {
  let component: DiseasetableComponent;
  let fixture: ComponentFixture<DiseasetableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiseasetableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiseasetableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
