import { Component, OnInit } from '@angular/core';
import { navData } from '../../navdata';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public navData = navData;
  public loggedIn = false;
  public username = "";

  constructor(private ls: LoginService) { }

  ngOnInit(): void {
    this.ls.isAuthenticated().subscribe(res => this.loggedIn = res);
    this.ls.getUsername().subscribe(res => this.username = res);
  }

  logOut() {
    this.ls.logOut();
  }
}
