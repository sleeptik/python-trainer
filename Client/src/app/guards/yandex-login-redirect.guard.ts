import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../services/auth.service";
import {switchMap} from "rxjs";

export const yandexLoginRedirectGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  const codeString = route.queryParamMap.get("code");

  if (codeString === null) {
    return router.navigateByUrl("/");
  }

  const code = parseInt(codeString!);

  return authService.login(code).pipe(
    switchMap(value => router.navigateByUrl(value ? "/assignments" : "/")
    )
  );
};
