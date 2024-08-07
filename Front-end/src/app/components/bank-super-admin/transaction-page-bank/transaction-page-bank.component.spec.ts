import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionPageBankComponent } from './transaction-page-bank.component';

describe('TransactionPageBankComponent', () => {
  let component: TransactionPageBankComponent;
  let fixture: ComponentFixture<TransactionPageBankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransactionPageBankComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransactionPageBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
