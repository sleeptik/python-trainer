import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError, map, of, tap} from "rxjs";
import {SimpleUserInfo} from "../models/simple-user-info";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private yandexRedirectLink: string | null = null;

  constructor(private readonly httpClient: HttpClient) {
  }
  //Сервис для обращения к контоллеру авторизации на беке

  getCurrentUser() {
    // TODO remake with interceptors, possibly.
    return this.httpClient.get<SimpleUserInfo>("api/auth/me").pipe(
      catchError(err => of(null)),
    );
  }

  getYandexRedirectLink() {
    if (this.yandexRedirectLink !== null)
      return of<string>(this.yandexRedirectLink);

    return this.httpClient.get("api/auth/yandex-redirect", {responseType: "text"})
      .pipe(tap(value => this.yandexRedirectLink = value));
  }

  login(code: number) {
    const params = new HttpParams().set("code", code);
    return this.httpClient.post<unknown>("api/auth/yandex-login", null, {observe: "response", params: params}).pipe(
      map(value => value.ok)
    );
  }

  logout() {
    return this.httpClient.post<unknown>("api/auth/logout", null, {observe: "response"}).pipe(
      map(value => value.ok)
    );
  }
}
