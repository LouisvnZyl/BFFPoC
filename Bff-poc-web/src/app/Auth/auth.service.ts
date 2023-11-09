import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private readonly http: HttpClient
  ) {}

  public url = 'https://localhost:7254/Auth';

  public signIn(): Observable<void> {
    return this.http.get<void>(`${this.url}/Login`, { withCredentials: true});
  }

  public testCookie() {
    return this.http.get<string>(`${this.url}/CookieCheck`, { withCredentials: true});
  }

}
