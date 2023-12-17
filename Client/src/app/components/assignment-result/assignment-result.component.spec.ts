import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentResultComponent } from './assignment-result.component';

describe('ExerciseResultComponent', () => {
  let component: AssignmentResultComponent;
  let fixture: ComponentFixture<AssignmentResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignmentResultComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignmentResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
