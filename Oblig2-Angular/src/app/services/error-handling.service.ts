import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError, defer } from 'rxjs';

@Injectable()
export class ErrorHandlingService {

  constructor() { }

  /** 
   * Custom operator to catch http status codes from failed http calls and output
   * user-friendly error messages from a stream.
   *
   * Takes as input the stream the error messages should be output from, and a function
   * that converts status codes to strings.
   * 
   * @param errorMessageObservable - The stream that should emit the error messages.
   * @param statusCodesToStringsFunction - Function that maps status codes to strings.
   *
   * It throws the error again after processing it. It's supposed to improve user experience,
   * not handle the errors. I just couldn't think of a better name than handleErrors. 🙂
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
