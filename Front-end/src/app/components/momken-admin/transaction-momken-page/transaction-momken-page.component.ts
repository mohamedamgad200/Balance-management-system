import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-transaction-momken-page',
  templateUrl: './transaction-momken-page.component.html',
  styleUrls: ['./transaction-momken-page.component.css'],
})
export class TransactionMomkenPageComponent implements OnInit {
  companyDeposits: any[] = [];
  companyDepits: any[] = [];
  filteredDeposits: any[] = [];
  filterDate: string = ''; // Default filter value for date
  filterTransactionType: number = 0; // Default filter value for transaction type
  filterStatus: string = 'Success'; // Default filter value for status

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private http: HttpClient
  ) {}

  formatDate(dateString: string): string {
    // Format date
    const options: Intl.DateTimeFormatOptions = {
      month: 'long',
      day: 'numeric',
      year: 'numeric',
    };
    return new Date(dateString).toLocaleDateString('en-US', options);
  }
  ngOnInit(): void {
    this.fetchCompanyDeposits();
    this.fetchCompanyDepits();
  }

  fetchCompanyDeposits(): void {
    // Fetch company deposits from API
    this.http
      .get<any[]>(
        'https://localhost:7182/api/ValuesController1/GetCompanyDeposits'
      )
      .subscribe(
        (data) => {
          // Filter deposits with status 'Success' and store them in the array
          this.companyDeposits = data.filter(
            (deposit) => deposit.status === 'success'
          );
          console.log(this.companyDeposits);
          // Apply filter after fetching deposits
          this.applyFilter();
        },
        (error) => {
          console.error('Error fetching company deposits:', error);
        }
      );
  }
  fetchCompanyDepits(): void {
    // Fetch company deposits from API
    this.http
      .get<any[]>(
        'https://localhost:7182/api/ValuesController1/GetCompanyDepits'
      )
      .subscribe(
        (data) => {
          // Map data and format date
          this.companyDepits = data.map((depit) => ({
            ...depit,
            formattedDate: this.formatDate(depit.date),
          }));
          // Apply filter after fetching deposits
          console.log(this.companyDepits);
          this.applyFilter();
        },
        (error) => {
          console.error('Error fetching company deposits:', error);
        }
      );
  }
  balancepage(): void {
    this.router.navigate(['/main-home-admin']);
  }

  reportpage(): void {
    this.router.navigate(['/report-page']);
  }

  depositepage(): void {
    this.router.navigate(['/deposite-page']);
  }

  transactionpage(): void {
    this.router.navigate(['/transaction-page']);
  }

  logout(): void {
    this.router.navigate(['']);
  }

  applyFilter(): void {
    // Combine deposits and depits into a single array for filtering
    const allTransactions = [...this.companyDeposits, ...this.companyDepits];

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
          transaction.transicationtype ===
          (Number(this.filterTransactionType) === 1 ? 'cashin' : 'cashout');
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
