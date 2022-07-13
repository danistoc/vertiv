import { TestBed } from '@angular/core/testing';

import { PasswordManagementServiceService } from './password-management-service.service';

describe('PasswordManagementServiceService', () => {
  let service: PasswordManagementServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PasswordManagementServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
