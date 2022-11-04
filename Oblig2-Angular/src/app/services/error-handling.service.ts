import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorHandlingService {
  // Credit: https://medium.com/ngconf/reactive-error-handling-in-angular-2bde9dd223a0

  private notification = new BehaviorSubject<number | null>(null);
  public notification$ = this.notification as Observable<number | null>;

  constructor() { }

  public handleError() {
    return (error: Error) => {
      if (error instanceof HttpErrorResponse) {
        this.notification.next(error.status);
      } else {
        this.notification.next(-1);
      }
      return throwError(error);
    }
  }
}
