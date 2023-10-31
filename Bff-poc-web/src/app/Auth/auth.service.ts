import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private readonly http: HttpClient,
    private readonly cookieService: CookieService
  ) {}

  public url = 'https://localhost:7254/Auth';

  public signIn(): Observable<void> {
    return this.http.get<void>(`${this.url}/Login`, {withCredentials: true});
  }

  public testCookie() {
    const headers = this.getCookieHeaders();

    return this.http.get<string>(`${this.url}/CookieCheck`, { headers: headers, withCredentials: true});
  }

  private getCookieHeaders(): HttpHeaders {
    const cookieValue = this.cookieService.get('auth_cookie');

    let cookies = document.cookie.split(';');

    console.log(cookies);

    const headers = new HttpHeaders({
      Cookie: `auth_cookie=${cookieValue}`,
    });

    return headers;
  }
}
