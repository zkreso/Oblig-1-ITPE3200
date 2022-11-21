import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FrontPage } from './frontpage/frontpage';
import { Login } from './login/login';
import { Meny } from './meny/meny';
import { DiseaseList } from './diseaselist/diseaselist';
import { AppRoutingModule } from './app-routing.module';
import { AuthComponent } from './services/auth/auth.component';
import { LoginService } from './services/LoginService';

@NgModule({
  declarations: [
    AppComponent,
    FrontPage,
    Login,
    Meny,
    DiseaseList
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  //entryComponents: [Modal] // merk!  
})
export class AppModule { }

