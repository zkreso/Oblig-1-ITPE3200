import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { User } from '../models';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private isLoggedIn = new BehaviorSubject(false);

  constructor(private http: HttpClient) { }

  public isAuthenticated() {
    return this.isLoggedIn;
  }

  public logIn(user: User) {
    this.http.post<boolean>("oblig/LogIn", user)
      .subscribe(res => this.isLoggedIn.next(res));
    return this.isLoggedIn;
  }

  public logOut(): void {
    this.isLoggedIn.next(false);
    this.http.get("oblig/LogOut");
  }
}
