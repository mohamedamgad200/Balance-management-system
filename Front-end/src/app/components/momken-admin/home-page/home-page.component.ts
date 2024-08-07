import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DatePipe, DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  providers: [DatePipe, DecimalPipe],
})
export class HomePageComponent implements OnInit {
  balance: string | null = null; // Change to string to store formatted balance
  lastUpdated: string | null = null;

  constructor(
    private router: Router,
    private http: HttpClient,
    private datePipe: DatePipe,
    private decimalPipe: DecimalPipe // Inject DecimalPipe
  ) {}

  ngOnInit() {
    this.fetchBalance();
  }

  fetchBalance() {
    this.http
      .get<any>(
        'https://localhost:7182/api/ValuesController1/GetCompanyBalance'
      )
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
  }

  logout() {
    this.router.navigate(['']);
  }

  reportPage() {
    this.router.navigate(['/report-page']);
  }

  depositepage() {
    this.router.navigate(['/deposite-page']);
  }
}
