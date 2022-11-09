import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './meny.html'
})
export class Meny {
  isExpanded = false;
  session = false;

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) { }

  ngOnInit() {
    this.http.get("oblig/IsLoggedIn")
      .subscribe(retur => {
        this.session = true;
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
