import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankReviewModalComponent } from './bank-review-modal.component';

describe('BankReviewModalComponent', () => {
  let component: BankReviewModalComponent;
  let fixture: ComponentFixture<BankReviewModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BankReviewModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BankReviewModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
