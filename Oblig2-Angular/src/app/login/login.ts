import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";
import { User } from "./User";

@Component({
  templateUrl: "login.html"
})
export class Login {
  form: FormGroup;

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {
    this.form = fb.group({
      username: ["", Validators.pattern('[A-Za-zÆØÅæøå. ]{2,20}')],
      password: ["", Validators.pattern('[A-Za-z0-9]{8,}')]
    });
  }

  login() {
    console.log("login() starts");
    console.log(this.form);

    const user = new User();
    user.Username = this.form.value.username
    user.Password = this.form.value.password;

    console.log("Found user: \n" + user.Username + "\n" + user.Password);

  }

}
