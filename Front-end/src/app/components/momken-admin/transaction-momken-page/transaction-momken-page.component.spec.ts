import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionMomkenPageComponent } from './transaction-momken-page.component';

describe('TransactionMomkenPageComponent', () => {
  let component: TransactionMomkenPageComponent;
  let fixture: ComponentFixture<TransactionMomkenPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransactionMomkenPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransactionMomkenPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
