import { Component } from '@angular/core';
import { AuthService } from './Auth/auth.service';
import { BehaviorSubject, Subject, filter, switchMap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Bff-poc-web';

  private readonly checkCookie$ = new BehaviorSubject<boolean>(false);

  constructor(private readonly authService: AuthService) {
    this.setUpAuthService();
    this.setUpCookieCheck();

    this.checkCookie$.subscribe((value) => console.log(value))
  }

  private setUpAuthService(): void {
    this.authService.signIn().subscribe(() => {
      console.log('Sign In Response');
      this.checkCookie$.next(true);
    });
  }

  private setUpCookieCheck(): void {
    this.checkCookie$
      .pipe(
        filter((value) => value),
        switchMap(() => {
          return this.authService.testCookie();
        })
      )
      .subscribe(() => {
        debugger;
        console.log('Cookie Checked');
      });
  }
}
