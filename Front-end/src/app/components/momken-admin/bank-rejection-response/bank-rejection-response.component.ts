import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-bank-rejection-response',
  templateUrl: './bank-rejection-response.component.html',
  styleUrl: './bank-rejection-response.component.css',
})
export class BankRejectionResponseComponent {
  constructor(
    public dialogRef: MatDialogRef<BankRejectionResponseComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }
  onSubmit() {
    console.log('submit form');
  }
}
