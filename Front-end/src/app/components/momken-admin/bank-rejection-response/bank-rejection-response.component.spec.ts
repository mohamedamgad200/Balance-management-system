import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankRejectionResponseComponent } from './bank-rejection-response.component';

describe('BankRejectionResponseComponent', () => {
  let component: BankRejectionResponseComponent;
  let fixture: ComponentFixture<BankRejectionResponseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BankRejectionResponseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BankRejectionResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
