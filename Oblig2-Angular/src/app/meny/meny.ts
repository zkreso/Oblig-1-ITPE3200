import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivationStart, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { LoginService } from '../services/LoginService';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './meny.html'
})
export class Meny {
  isExpanded = false;
  session = false;

  constructor(private http: HttpClient, private router: Router, private loginService: LoginService) {
 
  }

  logout() {
    this.loginService.logout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
