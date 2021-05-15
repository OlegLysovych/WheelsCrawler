import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  registerForm: FormGroup;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(12),
      ]),
      confirmPassword: new FormControl('', [
        Validators.required,
        this.matchValues('password'),
      ]),
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value == control?.parent?.controls[matchTo].value
        ? null
        : { isMatching: true };
    };
  }

  register() {
    this.model.Password = this.registerForm.value['password'];
    this.model.Username = this.registerForm.value['username'];
    this.accountService.register(this.model).subscribe(
      (response) => {
        console.log(response);
        this.toastr.success('registration successful');
        this.cancel();
      },
      (error) => {
        console.log(error);
        this.toastr.error(error.error);
      },
      () => {
        this.accountService.logIn(this.model).subscribe(() => {
          this.router.navigate(['/cars']);
        });
      }
    );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
