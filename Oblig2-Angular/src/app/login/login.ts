import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from "./User"

@Component({
  templateUrl: "login.html"
})
export class Login {
  inUser: User;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) {
    this.laster = false;
    this.inUser = {
      Id: 1,
      Username: "admin",
      Password: "admin"
    }
  }

}
