import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html'
})
export class WelcomeComponent implements OnInit {
  authLink: string | null = null;

  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.getYandexRedirectLink().subscribe(value => this.authLink = value);
  }
}
