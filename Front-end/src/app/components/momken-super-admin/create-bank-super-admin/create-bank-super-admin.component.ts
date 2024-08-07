import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-bank-super-admin',
  templateUrl: './create-bank-super-admin.component.html',
  styleUrls: ['./create-bank-super-admin.component.css'],
})
export class CreateBankSuperAdminComponent implements OnInit {
  bankForm!: FormGroup;
  apiUrl = 'https://localhost:7182/api/ValuesController1/CreateBank';
  successMessage = '';
  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.bankForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  createBank() {
    if (this.bankForm.invalid) {
      return;
    }

    const formData = this.bankForm.value;

    this.http.post<any>(this.apiUrl, formData).subscribe({
      next: (response) => {
        console.log('Bank created successfully:', response);
        // Redirect or perform other actions upon successful creation
        this.successMessage = 'Bank created successfully';
        this.clearFormFields();
        setTimeout(() => {
          this.successMessage = '';
        }, 3000);
      },
      error: (error) => {
        console.error('Error creating bank:', error);
        // Handle error appropriately
      },
    });
  }
  clearFormFields() {
    this.bankForm.reset();
  }
  companypage() {
    this.router.navigate(['/super-home-admin']);
  }

  bankpage() {
    this.router.navigate(['/Bank-creation']);
  }

  logout() {
    this.router.navigate(['']);
  }
}
