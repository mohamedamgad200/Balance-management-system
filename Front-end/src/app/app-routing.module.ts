import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomePageComponent } from './components/momken-admin/home-page/home-page.component';
import { HomePageSuperAdminComponent } from './components/momken-super-admin/home-page-super-admin/home-page-super-admin.component';
import { HomePageBankComponent } from './components/bank-super-admin/home-page-bank/home-page-bank.component';
import { ReportPageComponent } from './components/momken-admin/report-page/report-page.component';
import { TransactionMomkenPageComponent } from './components/momken-admin/transaction-momken-page/transaction-momken-page.component';
import { DepositsPageMomkenComponent } from './components/momken-admin/deposits-page-momken/deposits-page-momken.component';
import { TransactionPageBankComponent } from './components/bank-super-admin/transaction-page-bank/transaction-page-bank.component';
import { CreateBankSuperAdminComponent } from './components/momken-super-admin/create-bank-super-admin/create-bank-super-admin.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'main-home-admin', component: HomePageComponent },
  { path: 'super-home-admin', component: HomePageSuperAdminComponent },
  { path: 'bank-home-admin', component: HomePageBankComponent },
  { path: 'report-page', component: ReportPageComponent },
  { path: 'transaction-page', component: TransactionMomkenPageComponent },
  { path: 'transaction-page-main', component: DepositsPageMomkenComponent },
  { path: 'deposite-page', component: DepositsPageMomkenComponent },
  { path: 'transaction-page-bank', component: TransactionPageBankComponent },
  { path: 'Bank-creation', component: CreateBankSuperAdminComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
