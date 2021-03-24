import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LoginComponent } from './Login/Login.component';
import { DefaultComponent } from './layouts/default/default.component';
import { MsalGuard } from '@azure/msal-angular';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LogoutComponent } from './logout/logout.component';

const routes: Routes =[
  {
    path: '',component : LoginComponent, canActivate: [MsalGuard] 
  },
  {
    path: 'ADLogin',component : LoginComponent, canActivate: [MsalGuard] 
  },
  { path: 'login',component : LoginComponent, canActivate: [MsalGuard] },
  { path: 'login/:userid',     component: LoginComponent , canActivate: [MsalGuard] },
  { path: 'login/:weekid/:formheaderid/:userid',     component: LoginComponent , canActivate: [MsalGuard] },
  { path: 'logout',component : LogoutComponent },
  {
    path: 'dashboard',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  }, 
  {
    path: '',
    component: DefaultComponent,
    children: [{
      path: '',
      //loadChildren: './layouts/admin-layout/admin-layout.module#AdminLayoutModule'
      loadChildren: './layouts/default/default.module#DefaultModule'
    },
    { path: '**', component: LoginComponent },
  ]
  }
];

const isIframe = window !== window.parent && !window.opener;
@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
       useHash: true,
       // Don't perform initial navigation in iframes
      initialNavigation: !isIframe ? 'enabled' : 'disabled'
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
