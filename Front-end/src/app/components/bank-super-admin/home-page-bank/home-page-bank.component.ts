import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DatePipe, DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms'; // Make sure FormsModule is imported
@Component({
  selector: 'app-home-page-bank',
  templateUrl: './home-page-bank.component.html',
  styleUrls: ['./home-page-bank.component.css'],
  providers: [DatePipe, DecimalPipe],
})
export class HomePageBankComponent implements OnInit {
  lastUpdated: string | null = null;
  balance: string | null = null;
  actionType: string = 'increase'; // default value for action type
  amount: number | null = null; // default amount

  constructor(
    private router: Router,
    private datePipe: DatePipe,
    private decimalPipe: DecimalPipe,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.fetchBalance();
  }

  fetchBalance() {
    this.http
      .get<any>('https://localhost:7182/api/ValuesController1/GetBankBalance')
      .subscribe(
        (response) => {
          this.balance = this.decimalPipe.transform(response.balance, '1.0-0'); // Format balance
          this.updateLastUpdated();
        },
        (error) => {
          console.error('Error fetching balance', error);
        }
      );
  }

  updateLastUpdated() {
    const now = new Date();
    this.lastUpdated = this.datePipe.transform(now, 'MMMM d, y, h:mm:ss a');
    console.log(this.lastUpdated);
  }

  logout() {
    this.router.navigate(['']);
  }

  balancepage() {
    this.router.navigate(['/bank-home-admin']);
  }

  transactionpage() {
    this.router.navigate(['/transaction-page-bank']);
  }

  correctBalance() {
    const payload = { type: this.actionType, amount: this.amount };
    this.http
      .post(
        'https://localhost:7182/api/ValuesController1/CorrectBalance',
        payload
      )
      .subscribe(
        (response) => {
          console.log('Balance correction successful', response);
          this.amount = null;
          this.fetchBalance(); // Refresh balance after correction
        },
        (error) => {
          console.error('Error correcting balance', error);
        }
      );
  }
}
