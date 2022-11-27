import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Symptom, Disease, PageOptions, SymptomsTable, DiseaseEntity } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {

  constructor(private http: HttpClient) { }

  getAllDiseases(searchString?: string): Observable<Disease[]> {
    return this.http.get<Disease[]>("oblig/getAllDiseases" + (searchString ? `?searchString=${searchString}` : ""));
  }

  searchDisease(selectedSymptoms: Symptom[]): Observable<Disease[]> {
    return this.http.post<Disease[]>("oblig/searchDiseases", selectedSymptoms);
  }

  getSymptomsPage(options: PageOptions): Observable<SymptomsTable> {
    return this.http.post<SymptomsTable>("oblig/getSymptomsTable", options);
  }

  getDisease(id: number): Observable<Disease> {
    return this.http.get<Disease>("oblig/getDisease?id=" + id);
  }

  getRelatedSymptoms(id: number): Observable<Symptom[]> {
    return this.http.get<Symptom[]>("oblig/GetRelatedSymptoms?id=" + id);
  }

  createDisease(disease: DiseaseEntity): Observable<Disease> {
    return this.http.post<Disease>("oblig/CreateDisease", disease);
  }

  updateDisease(disease: DiseaseEntity): Observable<void> {
    return this.http.put<void>("oblig/UpdateDisease", disease);
  }

  deleteDisease(id: number): Observable<string> {
    return this.http.delete<string>("oblig/deleteDisease?id=" + id);
  }

  // method for testing errors

  public generateError(code: number): Observable<Response> {
    const error = new HttpErrorResponse({ status: code });
    return throwError(error) as any;
  }
}
