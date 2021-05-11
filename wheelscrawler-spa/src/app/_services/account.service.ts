import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  user: User = null;

  isLoginSubject = new BehaviorSubject<boolean>(this.hasToken());
  private currentUserSourceNew = new BehaviorSubject<User>(this.user);

  /**
   * if we have token the user is loggedIn
   * @returns {boolean}
   */
  private hasToken(): boolean {
    return !!localStorage.getItem('user');
  }

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSourceNew.asObservable();

  constructor(private http: HttpClient) {}

  logIn(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.isLoginSubject.next(true);
          this.currentUserSource.next(user);
          this.currentUserSourceNew.next(user);
        }
      })
    );
  }
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.isLoginSubject.next(true);
          this.currentUserSource.next(user);
          this.currentUserSourceNew.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    this.currentUserSource.next(user);
    this.currentUserSourceNew.next(user);
    this.isLoginSubject.next(true);
  }

  logOut() {
    localStorage.removeItem('user');
    this.isLoginSubject.next(false);
    this.currentUserSource.next(null);
    this.currentUserSourceNew.next(null);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }
  /**
   *
   * @returns {Observable<T>}
   */
  isLoggedIn(): Observable<boolean> {
    return this.isLoginSubject.asObservable();
  }
}
