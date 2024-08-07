import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePageSuperAdminComponent } from './home-page-super-admin.component';

describe('HomePageSuperAdminComponent', () => {
  let component: HomePageSuperAdminComponent;
  let fixture: ComponentFixture<HomePageSuperAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomePageSuperAdminComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomePageSuperAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
