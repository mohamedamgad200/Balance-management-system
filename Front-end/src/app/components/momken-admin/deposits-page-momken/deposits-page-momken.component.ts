import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { BankModalComponent } from '../bank-modal/bank-modal.component';
import { BankRejectionResponseComponent } from '../bank-rejection-response/bank-rejection-response.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-deposits-page-momken',
  templateUrl: './deposits-page-momken.component.html',
  styleUrls: ['./deposits-page-momken.component.css'],
})
export class DepositsPageMomkenComponent implements OnInit {
  companyDeposits: any[] = [];
  companyDepits: any[] = [];
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
          // Map data and format date
          this.companyDeposits = data.map((deposit) => ({
            ...deposit,
            formattedDate: this.formatDate(deposit.date),
          }));
          // Apply filter after fetching deposits
          console.log(this.companyDeposits);
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
          console.log(this.companyDeposits);
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

  depositeModal(): void {
    // Open the bank modal when the "Deposite" button is clicked
    const dialogRef = this.dialog.open(BankModalComponent, {
      width: '1000px',
      height: '650px', // Adjust the width as needed
      data: {}, // Pass any data to the modal if required
    });

    // Subscribe to the dialog close event
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      // Add any logic to handle dialog close if needed
    });
  }

  openRejectModal(message: string): void {
    // Open the BankRejectModalComponent
    const dialogRef = this.dialog.open(BankRejectionResponseComponent, {
      width: '600px',
      height: '400px', // Adjust the width as needed
      data: { message }, // Pass any data you want to the modal
    });

    // Optionally, you can subscribe to the dialog close event
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The bank reject modal was closed');
    });
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
