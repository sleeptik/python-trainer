import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {map, of, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private yandexRedirectLink: string | null = null;

  constructor(private readonly httpClient: HttpClient) {
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
}
