import { Component, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { LoginService } from '../LoginService';

@Injectable({
  providedIn: 'root'
})
export class AuthComponent implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) : Observable<boolean | UrlTree> {
    return this.loginService.isAuth().pipe(
      map(isLoggedIn => isLoggedIn ? isLoggedIn : this.router.createUrlTree(['']))
    );
  }
}
