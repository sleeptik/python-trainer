import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {BehaviorSubject} from "rxjs";
import {SimpleUserInfo} from "../../models/simple-user-info";

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html'
})
export class WelcomeComponent implements OnInit {
  isLoading = true;
  currentUser$ = new BehaviorSubject<SimpleUserInfo | null>(null);

  authLink: string | null = null;

  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(value => {
      this.currentUser$.next(value);
      this.isLoading = false;
    });

    this.authService.getYandexRedirectLink().subscribe(value => this.authLink = value);
  }
}
