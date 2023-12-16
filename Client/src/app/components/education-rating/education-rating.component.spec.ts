import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationRatingComponent } from './education-rating.component';

describe('EducationRatingComponent', () => {
  let component: EducationRatingComponent;
  let fixture: ComponentFixture<EducationRatingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EducationRatingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EducationRatingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
