import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";

interface LoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm: FormGroup<LoginForm>;

  constructor(private readonly authService: AuthService,
              private readonly router: Router,
              formBuilder: FormBuilder) {
    this.loginForm = formBuilder.group({
      email: formBuilder.control("", {nonNullable: true}),
      password: formBuilder.control("", {nonNullable: true})
    });
  }

  login() {
    const data = this.loginForm.value;
    this.authService.simpleLogin(data.email!, data.password!).subscribe(value =>
      this.router.navigateByUrl("/"));
  }
}
