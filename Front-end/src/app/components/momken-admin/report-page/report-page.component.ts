import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-report-page',
  templateUrl: './report-page.component.html',
  styleUrls: ['./report-page.component.css'],
})
export class ReportPageComponent implements OnInit {
  reportData: any = {};
  selectedDate: string = ''; // Initialize selectedDate as an empty string

  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    // No need to fetch data on component initialization
    // Fetching will be done when the date selection changes
  }

  logout() {
    this.router.navigate(['']);
  }

  balancepage() {
    this.router.navigate(['/main-home-admin']);
  }

  transactionpage() {
    this.router.navigate(['/transaction-page-main']);
  }

  // Method to fetch end day report data for a specific date
  getEndDayReport(date: string) {
    this.http
      .get<any>(
        `https://localhost:7182/api/ValuesController1/enddayreport?date=${date}`
      )
      .subscribe(
        (data) => {
          // Assign the response data to the reportData property
          this.reportData = data;
          console.log(data);
        },
        (error) => {
          // Handle errors if the request fails
          console.error('Error fetching end day report:', error);
        }
      );
  }

  // Method to handle date selection change
  onDateChange() {
    // Check if the selected date is not empty
    if (this.selectedDate) {
      // Fetch end day report data for the selected date
      this.getEndDayReport(this.selectedDate);
    } else {
      // Clear the reportData when the selection is empty
      this.reportData = {};
    }
  }
}
