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
    return this.http.get<void>(`${this.url}/Login`);
  }

  public testCookie() {
    this.getCookieHeaders();

    return this.http.get<Response>(`${this.url}/CookieCheck`);
  }

  private getCookieHeaders(): HttpHeaders {
    const cookieValue = this.cookieService.get('MyMagicalCookie');

    let cookies = document.cookie.split(';');

    console.log(cookies);

    const headers = new HttpHeaders({
      Authorization: `Bearer ${cookieValue}`,
    });

    return headers;
  }
}
