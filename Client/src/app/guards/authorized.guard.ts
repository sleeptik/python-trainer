import {inject} from "@angular/core";
import {CanActivateFn, Router} from '@angular/router';
import {map} from "rxjs";
import {AuthService} from "../services/auth.service";

export const authorizedGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.getCurrentUser().pipe(
    map(value => !!value ? true : router.parseUrl("/"))
  );
};
