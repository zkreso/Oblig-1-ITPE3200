import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";
import { Disease } from "../Disease";
import { Symptom } from "../Symptom";

@Component({
  templateUrl: "diseaselist.html"
})
export class DiseaseList {
  diseases: Disease[] | undefined;
  searchString: string;

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {
    this.searchString = "";
  }

  ngOnInit() {
    this.http.get<Disease[]>("api/oblig/GetAllDiseases/" + this.searchString)
      .subscribe(retur => {
        this.diseases = retur;
      },
        error => console.log(error)
      );
  }
}
