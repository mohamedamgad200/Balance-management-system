import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bank-reject-modal',
  templateUrl: './bank-reject-modal.component.html',
  styleUrls: ['./bank-reject-modal.component.css'],
})
export class BankRejectModalComponent {
  constructor(
    public dialogRef: MatDialogRef<BankRejectModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private http: HttpClient // Import HttpClient
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }

  onSubmit() {
    // Combine the note from the HTML with the predefined note or use 'no cash' if notes are not provided
    const combinedNote = this.data.notes ? this.data.notes : 'no cash';

    if (this.data.type === 'credit') {
      // Construct the API URL with query parameter
      const apiUrl = `https://localhost:7182/api/ValuesController1/AddNoteToRejectedDeposit?note=${encodeURIComponent(
        combinedNote
      )}`;

      // Make API call
      this.http
        .post<any>(apiUrl, {}) // Sending an empty object as the body since note is in the query parameter
        .subscribe({
          next: () => {
            console.log('API call successful');
            this.dialogRef.close(); // Close modal after successful submission
          },
          error: (error) => {
            console.error('Error occurred while submitting:', error);
            // Handle error here
          },
        });
    } else {
      // Construct the API URL with query parameter
      const apiUrl = `https://localhost:7182/api/ValuesController1/AddNoteToRejectedDeposit?note=${encodeURIComponent(
        combinedNote
      )}`;

      // Make API call
      this.http
        .post<any>(apiUrl, {}) // Sending an empty object as the body since note is in the query parameter
        .subscribe({
          next: () => {
            console.log('API call successful');
            this.dialogRef.close(); // Close modal after successful submission
          },
          error: (error) => {
            console.error('Error occurred while submitting:', error);
            // Handle error here
          },
        });
    }
  }
}
