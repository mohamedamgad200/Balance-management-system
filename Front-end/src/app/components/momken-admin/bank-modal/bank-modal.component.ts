import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SafeUrl, DomSanitizer } from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bank-modal',
  templateUrl: './bank-modal.component.html',
  styleUrls: ['./bank-modal.component.css'],
})
export class BankModalComponent implements OnInit {
  bankName: string = '';
  accountNumber: string = '';
  amount: number = 0;
  receipt: File | null = null;
  imageUrl: SafeUrl | null = null;

  constructor(
    public dialogRef: MatDialogRef<BankModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private sanitizer: DomSanitizer,
    private http: HttpClient
  ) {}

  ngOnInit(): void {}

  onClose(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    // Create a FormData object to send file and other data
    const formData = new FormData();
    if (this.receipt) {
      formData.append('File', this.receipt);
    }
    formData.append('Amount', this.amount.toString());
    formData.append('BankAccountNumber', this.accountNumber);
    formData.append('BankName', this.bankName);

    // Make HTTP POST request with FormData
    this.http
      .post<any>(
        'https://localhost:7182/api/ValuesController1/UploadFile',
        formData
      )
      .subscribe(
        (response) => {
          console.log(formData);
          console.log('Response:', response);
          if (response.message === 'File uploaded successfully') {
            console.log('File uploaded successfully');
          } else {
            console.log('Error: No file uploaded');
          }
        },
        (error) => {
          console.error('Error:', error);
          console.log('Error: No file uploaded');
        }
      );

    this.dialogRef.close();
  }
  onFileSelected(event: any): void {
    this.receipt = event.target.files[0];
    this.generateImageUrl();
  }

  generateImageUrl(): void {
    if (this.receipt) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        // Sanitize the URL before setting it
        this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(e.target.result);
      };
      reader.readAsDataURL(this.receipt);
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    // Add styles for drag over if needed
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    // Remove styles for drag over if needed
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    const files = event.dataTransfer?.files;
    this.handleFiles(files);
    // Remove styles for drag over if needed
  }

  handleFiles(files: FileList | null | undefined) {
    if (files && files.length > 0) {
      this.receipt = files[0];
      this.generateImageUrl();
    }
  }
}
