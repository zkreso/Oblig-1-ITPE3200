import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './login/login';
import { FrontPage } from './frontpage/frontpage';
import { DiseaseList } from './diseaselist/diseaselist';

const appRoots: Routes = [
  { path: 'frontpage', component: FrontPage },
  { path: 'login', component: Login },
  { path: 'diseaselist', component: DiseaseList},
  { path: '', redirectTo: '/frontpage', pathMatch: 'full' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }