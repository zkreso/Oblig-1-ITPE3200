import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError, defer } from 'rxjs';

@Injectable()
export class ErrorHandlingService {

  constructor() { }

  /** 
   * Custom operator to detect errors from failed http calls and output user-friendly error
   * messages from a stream passed as parameter.
   *
   * Takes as input the stream the error messages should be output from, and a dictionary (Map)
   * that contains the strings the different http status codes should be mapped to.
   * 
   * @param errorMessage$ - The stream that should emit the error messages.
   * @param dict - Dictionary with http status codes as keys and strings as values.
   *
   * It throws the error again after processing it. It's supposed to improve user experience,
   * not handle the errors. I just couldn't think of a better name than handleErrors. 🙂
   */

  public handleErrors<T>
    (errorMessage$: BehaviorSubject<string | null>, dict: Map<number, string>):
    (source: Observable<T>) => Observable<T>
  {
    return (source: Observable<T>) =>
      defer(() => {
        errorMessage$.next(null);
        return source;
      }).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          let errorMessage = dict.get(error.status) || "An unknown error occured";
          errorMessage$.next(errorMessage);
        } else {
          let errorMessage = "An unknown error occured";
          errorMessage$.next(errorMessage);
        }
        return throwError(error);
      })
    );
  }

}
