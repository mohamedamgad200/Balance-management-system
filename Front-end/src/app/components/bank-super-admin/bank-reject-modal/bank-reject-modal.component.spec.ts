import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankRejectModalComponent } from './bank-reject-modal.component';

describe('BankRejectModalComponent', () => {
  let component: BankRejectModalComponent;
  let fixture: ComponentFixture<BankRejectModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BankRejectModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BankRejectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
