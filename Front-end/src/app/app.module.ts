import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomePageSuperAdminComponent } from './components/momken-super-admin/home-page-super-admin/home-page-super-admin.component';
import { HomePageBankComponent } from './components/bank-super-admin/home-page-bank/home-page-bank.component';
import { ReportPageComponent } from './components/momken-admin/report-page/report-page.component';
import { TransactionMomkenPageComponent } from './components/momken-admin/transaction-momken-page/transaction-momken-page.component';
import { DepositsPageMomkenComponent } from './components/momken-admin/deposits-page-momken/deposits-page-momken.component';
import { TransactionPageBankComponent } from './components/bank-super-admin/transaction-page-bank/transaction-page-bank.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BankModalComponent } from './components/momken-admin/bank-modal/bank-modal.component';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { BankReviewModalComponent } from './components/bank-super-admin/bank-review-modal/bank-review-modal.component';
import { BankRejectModalComponent } from './components/bank-super-admin/bank-reject-modal/bank-reject-modal.component';
import { CreateBankSuperAdminComponent } from './components/momken-super-admin/create-bank-super-admin/create-bank-super-admin.component';
import { HttpClientModule } from '@angular/common/http';
import { BankRejectionResponseComponent } from './components/momken-admin/bank-rejection-response/bank-rejection-response.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomePageSuperAdminComponent,
    HomePageBankComponent,
    ReportPageComponent,
    TransactionMomkenPageComponent,
    DepositsPageMomkenComponent,
    TransactionPageBankComponent,
    BankModalComponent,
    BankReviewModalComponent,
    BankRejectModalComponent,
    CreateBankSuperAdminComponent,
    BankRejectionResponseComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDialogModule,
    HttpClientModule,
  ],
  providers: [provideClientHydration(), provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
