import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError, defer } from 'rxjs';

@Injectable()
export class ErrorHandlingService {

  constructor() { }

  /** 
   * Emits http errors to specified subject as strings, based on supplied conversion function.
   * Sends null at the start of new requests to reset the status.
   * 
   * @param errorMessageObservable - The subject that should emit the error messages.
   * @param statusCodesToStringsFunction - Function that takes as input http status codes
   * and outputs strings.
   */

  public handleErrors<T>(
    errorMessageObservable: BehaviorSubject<string | null>,
    statusCodesToStringsFunction: (number: HttpStatusCode) => string
  ): (source: Observable<T>) => Observable<T> {
    return (source: Observable<T>) =>
      defer(() => {
        errorMessageObservable.next(null);
        return source;
      }).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          errorMessageObservable.next(statusCodesToStringsFunction(error.status));
        } else {
          errorMessageObservable.next(statusCodesToStringsFunction(-1));
        }
        return throwError(error);
      })
    );
  }

}
