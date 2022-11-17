import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingbarComponent } from './pagingbar.component';

describe('PagingbarComponent', () => {
  let component: PagingbarComponent;
  let fixture: ComponentFixture<PagingbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagingbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PagingbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
