import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouteStrings } from './navdata';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CalculatorComponent } from './components/calculator/calculator.component';
import { DiseaselistComponent } from './pages/diseaselist/diseaselist.component';
import { EditpageComponent } from './pages/editpage/editpage.component';
import { DiseasedetailsComponent } from './pages/diseasedetails/diseasedetails.component';
import { AddpageComponent } from './pages/addpage/addpage.component';
import { SelectedsymptomsComponent } from './components/selectedsymptoms/selectedsymptoms.component';
import { SymptomstableComponent } from './components/symptomstable/symptomstable.component';
import { SearchbarComponent } from './components/searchbar/searchbar.component';
import { PagingbarComponent } from './components/pagingbar/pagingbar.component';
import { DiseasetableComponent } from './components/diseasetable/diseasetable.component';
import { IndexComponent } from './pages/index/index.component';
import { SymptomsubpageComponent } from './components/symptomsubpage/symptomsubpage.component';
import { AuthguardGuard } from './services/authguard.guard';
import { LoginComponent } from './pages/login/login.component';
import { GlobalHttpInterceptorService } from './services/global-http-interceptor.service';
import { NotfoundComponent } from './pages/notfound/notfound.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DeletemodalComponent } from './components/diseasetable/deletemodal/deletemodal.component';
import { DiseaseFormComponent } from './components/disease-form/disease-form.component';

const appRoutes: Routes = [
  { path: RouteStrings.Home, component: IndexComponent },
  { path: RouteStrings.AddPage, component: AddpageComponent, canActivate: [AuthguardGuard] },
  { path: RouteStrings.EditPage, component: EditpageComponent, canActivate: [AuthguardGuard] },
  { path: RouteStrings.DiseaseList, component: DiseaselistComponent },
  { path: RouteStrings.DiseaseDetails, component: DiseasedetailsComponent },
  { path: RouteStrings.NotFound, component: NotfoundComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '404', pathMatch: 'full' }
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CalculatorComponent,
    DiseaselistComponent,
    EditpageComponent,
    DiseasedetailsComponent,
    AddpageComponent,
    SelectedsymptomsComponent,
    SymptomstableComponent,
    SearchbarComponent,
    PagingbarComponent,
    DiseasetableComponent,
    IndexComponent,
    SymptomsubpageComponent,
    LoginComponent,
    NotfoundComponent,
    DeletemodalComponent,
    DiseaseFormComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: GlobalHttpInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
