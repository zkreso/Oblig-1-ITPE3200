import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { User } from '../models';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private isLoggedIn$ = new BehaviorSubject(false);
  private username$ = new BehaviorSubject("");

  constructor(private http: HttpClient) { }

  public isAuthenticated() {
    return this.isLoggedIn$;
  }

  public getUsername() {
    return this.username$;
  }

  public logIn(user: User) {
    this.http.post<boolean>("oblig/LogIn", user)
      .subscribe(res => {
        this.isLoggedIn$.next(res);
        this.username$.next(user.username)
      });
    return this.isLoggedIn$;
  }

  public logOut(): void {
    this.isLoggedIn$.next(false);
    this.username$.next("");
    this.http.get("oblig/LogOut");
  }
}
