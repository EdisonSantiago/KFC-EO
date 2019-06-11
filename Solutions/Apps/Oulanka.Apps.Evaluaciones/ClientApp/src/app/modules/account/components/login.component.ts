import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../domain/user';
import { UserView } from '../domain/userView';
import { OperationResult } from '../../../core/domain/operationResult';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { pipe } from 'rxjs';
import { first } from 'rxjs/operators';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  userView: any;
  returnUrl: string;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.authenticationService.logout();
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  get f() { return this.loginForm.controls; }

  login(): void {
    this.submitted = true;

    if (this.loginForm.invalid) { return; }

    this.loading = true;
    const authenticationResult: OperationResult = new OperationResult(
      false,
      ''
    );

    this.authenticationService
      .login(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        res => {
          authenticationResult.Succeeded = true;
          authenticationResult.Message =
            'Ingreso de sesión ' + this.f.username.value;
          this.userView = res;
          // this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          console.log('error capturado ' + error.error_description);
          this.loading = false;
        },
        () => {
          if (authenticationResult.Succeeded) {
            this.notificationService.printSuccessMessage(
              'Bienvenido ' + this.f.username.value + '!'
            );
            localStorage.setItem('userView', JSON.stringify(this.userView));
            this.router.navigate(['home']);
          } else {
            this.notificationService.printErrorMessage(
              authenticationResult.Message
            );
          }
        }
      );
  }
}
