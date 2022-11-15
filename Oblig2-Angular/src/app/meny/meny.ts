import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivationStart, Router } from '@angular/router';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './meny.html'
})
export class Meny {
  isExpanded = false;
  session = false;

  constructor(private http: HttpClient, private router: Router) {
    router.events.subscribe(event => {
      if (event instanceof ActivationStart) {
        console.log("Routing changed");
        this.isLoggedIn();
      }
    });
  }

  isLoggedIn() {
    this.http.get<boolean>("api/oblig/IsLoggedIn/")
      .subscribe(retur => {
        if (retur) {
          console.log("Session is valid");
          this.session = true;
        }
        else {
          console.log("Session is not valid")
        }
      },
        error => console.log(error)
      );
  }

  logout() {
    this.http.get<boolean>("api/LogOut/")
      .subscribe(retur => {
        if (retur) {
          this.session = false;
          console.log("Logged out");
        }
      },
        error => console.log(error)
      );
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
