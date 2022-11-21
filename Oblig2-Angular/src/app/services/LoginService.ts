import { Component, Injectable, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject } from "rxjs";
import { User } from '../User';

@Injectable({
  providedIn: 'root'
})
export class LoginService{

  private session = new BehaviorSubject(false);

  constructor(private http: HttpClient, private router: Router){ }

  isAuth() {
    return this.session;
  }

  /*
  isLoggedIn() {
    this.http.get<boolean>("api/oblig/IsLoggedIn/")
      .subscribe(retur => {
        if (retur) {
          console.log("Session is valid");
          this.session.next(false);
        }
        else {
          console.log("Session is not valid")
        }
      },
        error => console.log(error)
      );
  }
  */

  logout() {
    this.http.get<boolean>("api/oblig/LogOut/")
      .subscribe(retur => {
      this.session.next(false)
    },
      error => console.log(error)
    );
  }

  login(user: User) {

    this.http.post<boolean>("api/oblig/LogIn/", user)
      .subscribe(retur => {
        if (retur) {
          this.router.navigate(["/diseaselist"]);
          this.session.next(true);
        }
        else {
          console.log("Did not login");
        }
      },
        error => console.log(error)
    );
  }
}
