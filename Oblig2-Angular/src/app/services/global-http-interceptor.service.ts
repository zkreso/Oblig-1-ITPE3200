import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class GlobalHttpInterceptorService {

  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(

      catchError((error) => {

        if (error instanceof HttpErrorResponse) {
          switch (error.status) {
            case 401: // not authenticated
              this.router.navigate(['login']);
              break;
            case 403: // not authorized
              this.router.navigate(['login']);
              break;
            case 404: // not found
              this.router.navigate([''])
              break;
          }
        }

        return throwError(error)
      })

    );
  }
}
