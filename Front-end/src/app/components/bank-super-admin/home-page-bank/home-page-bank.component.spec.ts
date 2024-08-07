import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePageBankComponent } from './home-page-bank.component';

describe('HomePageBankComponent', () => {
  let component: HomePageBankComponent;
  let fixture: ComponentFixture<HomePageBankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomePageBankComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomePageBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
