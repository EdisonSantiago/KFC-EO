import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from 'src/app/modules/account/domain/user';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  public login(username: string, password: string) {
    let user: User;

    const body = new HttpParams()
      .set('username', username)
      .set('password', password)
      .set('grant_type', 'password');

    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      Accept: 'application/json'
    });
    const options = { headers };

    return this.http
      .post<any>(environment.APIEndPoint + 'token', body.toString(), options)
      .pipe(
        map(res => {
          if (res && res.access_token) {
            localStorage.setItem(
              'access_token',
              JSON.stringify(res.access_token)
            );
            localStorage.setItem('expires_in', JSON.stringify(res.expires_in));
            localStorage.setItem('username', username);

            user = new User(username, '');
            user.Username = username;
            user.Token = res.access_token;
            user.ExpiresIn = res.expires_in;
            localStorage.setItem('currentUser', JSON.stringify(user));

            this.currentUserSubject.next(user);
          }
          return user;
        })
      );
  }

  public logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('cadena');
    localStorage.removeItem('access_token');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('expires_in');
    localStorage.removeItem('userView');
    localStorage.removeItem('username');

    this.currentUserSubject.next(null);
  }
}
