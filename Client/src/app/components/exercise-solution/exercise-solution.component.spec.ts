import {ComponentFixture, TestBed} from '@angular/core/testing';

import {ExerciseSolutionComponent} from './exercise-solution.component';

describe('ExerciseSolutionComponent', () => {
  let component: ExerciseSolutionComponent;
  let fixture: ComponentFixture<ExerciseSolutionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExerciseSolutionComponent]
    });
    fixture = TestBed.createComponent(ExerciseSolutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
