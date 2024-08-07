import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  signInForm: FormGroup;
  successMessage = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private http: HttpClient // Inject HttpClient
  ) {
    this.signInForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  signIn() {
    if (this.signInForm.invalid) {
      return;
    }

    const { email, password } = this.signInForm.value;

    // Prepare the data object
    const requestData = {
      email: email,
      password: password,
    };

    // Send data to the server
    this.http
      .post<any>(
        'https://localhost:7182/api/ValuesController1/Authenticate',
        requestData
      )
      .subscribe({
        next: (response: any) => {
          // Adjust the type of 'response' if necessary
          // Handle successful response
          console.log(response);
          // Redirect based on response data
          if (response.role === 'superadmin') {
            this.router.navigate(['/super-home-admin']);
          } else if (response.role === 'Bank') {
            this.router.navigate(['/bank-home-admin']);
          } else if (response.role === 'Company') {
            this.router.navigate(['/main-home-admin']);
          } else {
            this.successMessage = 'Invalid Username or password';
            setTimeout(() => {
              this.successMessage = '';
            }, 3000);
          }
        },
        error: (error) => {
          // Handle error
          console.error('API error:', error);
          // Handle error in your application
          this.successMessage = 'Invalid Username or password';
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
      });
  }
}
