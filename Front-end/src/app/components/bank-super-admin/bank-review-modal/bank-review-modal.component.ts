import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialog,
} from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http'; // Import HttpClient for making HTTP requests
import { BankRejectModalComponent } from '../bank-reject-modal/bank-reject-modal.component';

@Component({
  selector: 'app-bank-review-modal',
  templateUrl: './bank-review-modal.component.html',
  styleUrls: ['./bank-review-modal.component.css'],
})
export class BankReviewModalComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogReff: MatDialogRef<BankReviewModalComponent>,
    private dialog: MatDialog,
    private http: HttpClient // Inject HttpClient
  ) {}

  onClose(): void {
    this.dialogReff.close();
  }

  onApprove(): void {
    if (this.data.type === 'credit') {
      const depositId = this.data.id;
      const newStatus = 'success';
      const apiUrl = `https://localhost:7182/api/ValuesController1/UpdateDepositStatus?depositId=${depositId}&newStatus=${newStatus}`;
      console.log(depositId, newStatus);
      // Make the POST request with FormData
      this.http.post<any>(apiUrl, {}).subscribe(
        (response: any) => {
          // Check if the response indicates an error
          if (response.message !== 'Deposit status updated successfully') {
            console.error('Error updating deposit status:', response);
            // Handle the error accordingly
          } else {
            console.log('Deposit status updated successfully:', response);
            this.dialogReff.close();
          }
        },
        (error) => {
          // Handle network errors or unexpected errors
          console.error('Error updating deposit status:', error);
        }
      );
    } else {
      const depitId = this.data.id;
      const newStatus = 'success';
      const apiUrl = `https://localhost:7182/api/ValuesController1/UpdateDepitStatus?depitId=${depitId}&newStatus=${newStatus}`;
      console.log(depitId, newStatus);
      // Make the POST request with FormData
      this.http.post<any>(apiUrl, {}).subscribe(
        (response: any) => {
          // Check if the response indicates an error
          if (response.message !== 'Depit status updated successfully') {
            console.error('Error updating deposit status:', response);
            // Handle the error accordingly
          } else {
            console.log('Depit status updated successfully:', response);
            this.dialogReff.close();
          }
        },
        (error) => {
          // Handle network errors or unexpected errors
          console.error('Error updating deposit status:', error);
        }
      );
    }
  }

  onReject(): void {
    if (this.data.type === 'credit') {
      const depositId = this.data.id;
      const newStatus = 'rejected';
      const apiUrl = `https://localhost:7182/api/ValuesController1/UpdateDepositStatus?depositId=${depositId}&newStatus=${newStatus}`;
      console.log(depositId, newStatus);
      // Make the POST request with FormData
      this.http.post<any>(apiUrl, {}).subscribe(
        (response: any) => {
          // Check if the response indicates an error
          if (response.message !== 'Deposit status updated successfully') {
            console.error('Error updating deposit status:', response);
            // Handle the error accordingly
          } else {
            console.log('Deposit status updated successfully:', response);
            this.dialogReff.close();
          }
        },
        (error) => {
          // Handle network errors or unexpected errors
          console.error('Error updating Deposit status:', error);
        }
      );
      this.dialogReff.close();
      this.openRejectModal();
    } else {
      const depitId = this.data.id;
      const newStatus = 'rejected';
      const apiUrl = `https://localhost:7182/api/ValuesController1/UpdateDepitStatus?depitId=${depitId}&newStatus=${newStatus}`;
      console.log(depitId, newStatus);
      // Make the POST request with FormData
      this.http.post<any>(apiUrl, {}).subscribe(
        (response: any) => {
          // Check if the response indicates an error
          if (response.message !== 'Depit status updated successfully') {
            console.error('Error updating depit status:', response);
            // Handle the error accordingly
          } else {
            console.log('Depit status updated successfully:', response);
            this.dialogReff.close();
          }
        },
        (error) => {
          // Handle network errors or unexpected errors
          console.error('Error updating depit status:', error);
        }
      );
      this.dialogReff.close();
      this.openRejectModal();
    }
  }

  openRejectModal(): void {
    // Open the BankRejectModalComponent
    const dialogRef = this.dialog.open(BankRejectModalComponent, {
      width: '600px',
      height: '400px',
      data: {},
    });

    // Optionally, you can subscribe to the dialog close event
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The bank reject modal was closed');
    });
  }
}
