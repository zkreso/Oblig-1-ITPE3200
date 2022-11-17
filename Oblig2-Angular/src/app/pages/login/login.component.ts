import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../../models';
import { LoginService } from '../../services/login.service';
  
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private ls: LoginService, private router: Router) { }

  ngOnInit(): void {
  }

  logIn() {
    if (this.form.invalid) {
      return;
    }

    let user: User = {
      username : this.form.controls.username.value!,
      password : this.form.controls.password.value!
    }

    if (this.ls.logIn(user)) {
      this.router.navigate(['']);
    }
  }

}
