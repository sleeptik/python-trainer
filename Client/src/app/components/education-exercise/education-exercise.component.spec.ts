import {ComponentFixture, TestBed} from '@angular/core/testing';

import {EducationExerciseComponent} from './education-exercise.component';

describe('EducationExerciseComponent', () => {
  let component: EducationExerciseComponent;
  let fixture: ComponentFixture<EducationExerciseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EducationExerciseComponent]
    });
    fixture = TestBed.createComponent(EducationExerciseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
