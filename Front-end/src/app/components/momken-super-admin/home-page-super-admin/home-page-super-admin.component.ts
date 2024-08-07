import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home-page-super-admin',
  templateUrl: './home-page-super-admin.component.html',
  styleUrl: './home-page-super-admin.component.css',
})
export class HomePageSuperAdminComponent implements OnInit {
  companyForm!: FormGroup;
  apiUrl = 'https://localhost:7182/api/ValuesController1/CreateCompany';
  successMessage = '';
  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {}
  ngOnInit() {
    this.companyForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }
  createCompany() {
    if (this.companyForm.invalid) {
      return;
    }

    const formData = this.companyForm.value;

    this.http.post<any>(this.apiUrl, formData).subscribe({
      next: (response) => {
        console.log('Company created successfully:', response);
        // Redirect or perform other actions upon successful creation
        this.successMessage = 'Company created successfully';
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
    this.companyForm.reset();
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
