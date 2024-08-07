import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { BankReviewModalComponent } from '../bank-review-modal/bank-review-modal.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-transaction-page-bank',
  templateUrl: './transaction-page-bank.component.html',
  styleUrls: ['./transaction-page-bank.component.css'],
})
export class TransactionPageBankComponent implements OnInit {
  bankDeposits: any[] = [];
  bankDepits: any[] = [];
  filterDate: string = ''; // Default filter value for date
  filterTransactionType: number = 0; // Default filter value for transaction type
  filterStatus: string = 'All'; // Default filter value for status
  filteredDeposits: any[] = [];

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.fetchBankDeposits();
    this.fetchBankDepits();
  }

  fetchBankDeposits(): void {
    // Fetch bank deposits from API
    this.http
      .get<any[]>(
        'https://localhost:7182/api/ValuesController1/GetBankDeposits'
      )
      .subscribe(
        (data) => {
          // Map data and format date
          this.bankDeposits = data.map((deposit) => ({
            ...deposit,
            formattedDate: this.formatDate(deposit.date),
          }));
          // Apply filter after fetching deposits
          console.log(this.bankDeposits);
          this.applyFilter();
        },
        (error) => {
          console.error('Error fetching bank deposits:', error);
        }
      );
  }
  fetchBankDepits(): void {
    // Fetch company deposits from API
    this.http
      .get<any[]>('https://localhost:7182/api/ValuesController1/GetBankDepits')
      .subscribe(
        (data) => {
          // Map data and format date
          this.bankDepits = data.map((depit) => ({
            ...depit,
            formattedDate: this.formatDate(depit.date),
          }));
          // Apply filter after fetching deposits
          console.log(this.bankDepits);
          this.applyFilter();
        },
        (error) => {
          console.error('Error fetching company deposits:', error);
        }
      );
  }
  formatDate(dateString: string): string {
    // Format date
    const options: Intl.DateTimeFormatOptions = {
      month: 'long',
      day: 'numeric',
      year: 'numeric',
    };
    return new Date(dateString).toLocaleDateString('en-US', options);
  }

  balancepage(): void {
    this.router.navigate(['/bank-home-admin']);
  }

  transactionpage(): void {
    this.router.navigate(['/transaction-page-bank']);
  }

  logout(): void {
    this.router.navigate(['']);
  }

  openBankReviewModal(deposit: any): void {
    // Open bank review modal
    const dialogRef = this.dialog.open(BankReviewModalComponent, {
      width: '1000px',
      height: '650px',
      data: deposit,
    });
    dialogRef.afterClosed().subscribe(() => {
      console.log('The bank review modal was closed');
    });
  }

  applyFilter(): void {
    // Combine deposits and depits into a single array for filtering
    const allTransactions = [...this.bankDeposits, ...this.bankDepits];

    // Filter transactions based on filterDate, filterTransactionType, and filterStatus
    this.filteredDeposits = allTransactions.filter((transaction) => {
      let dateMatch = true;
      if (this.filterDate) {
        // Format the filterDate to match the API's date format
        const formattedFilterDate = new Date(this.filterDate)
          .toISOString()
          .split('T')[0];
        dateMatch = transaction.date.split('T')[0] === formattedFilterDate;
      }
      let transactionTypeMatch = true;
      if (Number(this.filterTransactionType) !== 0) {
        transactionTypeMatch =
          transaction.type ===
          (Number(this.filterTransactionType) === 1 ? 'debit' : 'credit');
      }
      let statusMatch = true;
      if (this.filterStatus !== 'All') {
        statusMatch =
          transaction.status.toLowerCase() === this.filterStatus.toLowerCase();
      }
      return dateMatch && transactionTypeMatch && statusMatch;
    });
  }
}
