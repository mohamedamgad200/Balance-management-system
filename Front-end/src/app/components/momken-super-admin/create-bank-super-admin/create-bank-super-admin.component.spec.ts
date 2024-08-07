import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBankSuperAdminComponent } from './create-bank-super-admin.component';

describe('CreateBankSuperAdminComponent', () => {
  let component: CreateBankSuperAdminComponent;
  let fixture: ComponentFixture<CreateBankSuperAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateBankSuperAdminComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateBankSuperAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
