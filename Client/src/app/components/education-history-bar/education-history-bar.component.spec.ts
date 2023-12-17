import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationHistoryBarComponent } from './education-history-bar.component';

describe('EducationHistoryComponent', () => {
  let component: EducationHistoryBarComponent;
  let fixture: ComponentFixture<EducationHistoryBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EducationHistoryBarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EducationHistoryBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
