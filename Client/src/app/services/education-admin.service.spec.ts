import { TestBed } from '@angular/core/testing';

import { EducationAdminService } from './education-admin.service';

describe('EducationAdminService', () => {
  let service: EducationAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EducationAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
