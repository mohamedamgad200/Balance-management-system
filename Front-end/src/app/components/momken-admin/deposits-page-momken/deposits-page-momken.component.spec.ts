import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositsPageMomkenComponent } from './deposits-page-momken.component';

describe('DepositsPageMomkenComponent', () => {
  let component: DepositsPageMomkenComponent;
  let fixture: ComponentFixture<DepositsPageMomkenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepositsPageMomkenComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepositsPageMomkenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
