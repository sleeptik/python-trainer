import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationExercisesComponent } from './education-exercises.component';

describe('EducationExercisesComponent', () => {
  let component: EducationExercisesComponent;
  let fixture: ComponentFixture<EducationExercisesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EducationExercisesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EducationExercisesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
