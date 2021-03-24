import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import {PortalModule} from '@angular/cdk/portal';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { environment } from 'environments/environment';


import { DashboardComponent } from './dashboard/dashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { TableListComponent } from './table-list/table-list.component';
import { NewFormComponent } from './new-form/new-form.component';
import { TypographyComponent } from './typography/typography.component';
import { IconsComponent } from './icons/icons.component';
import { MapsComponent } from './maps/maps.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { UpgradeComponent } from './upgrade/upgrade.component';
import {
  AgmCoreModule
} from '@agm/core';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LoginComponent } from './Login/Login.component';

import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { AuthService } from './services/auth.service';
import { SpinnerService } from './services/spinner.service';
import { OverlayModule } from '@angular/cdk/overlay';
import { ModelServiceService} from './services/ModelService.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


import { AppConfig } from './app.config';
import { IAppConfig } from './model/app-config';

import { MsalModule, MsalInterceptor, MsalService } from '@azure/msal-angular';
import { tap } from 'rxjs/operators';
import { LogoutComponent } from './logout/logout.component';
const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

export const protectedResourceMap: any =
  [
    [environment.mainUrl, environment.scopeUri]
  ];

  
@NgModule({
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatMenuModule, MatProgressBarModule, MatProgressSpinnerModule,
    AgmCoreModule.forRoot({
      apiKey: 'YOUR_GOOGLE_MAPS_API_KEY'
    }),
    OverlayModule,
    PortalModule,
    MatSidenavModule,
    MsalModule.forRoot({
      auth: {
        clientId: environment.uiClienId,
        authority: environment.authority,
        redirectUri: environment.redirectUrl,
        postLogoutRedirectUri: environment.postLogoutRedirectUri,
      //cacheLocation: 'localStorage',
        validateAuthority: true,
        navigateToLoginRequestUrl: true
      }, cache: {
          //cacheLocation: 'sessionStorage',
          cacheLocation: 'localStorage',
          storeAuthStateInCookie: true, // set to true for IE 11
        },
        framework: {
          isAngular: true
        },
  }, {
    //popUp: !isIE,
    popUp: false,
    consentScopes: [
      'user.read',
      'openid',
      'profile',
      environment.scopeUri
    ],
    protectedResourceMap: [
      //[environment.mainUrl, ['api://e45ca2ca-bfa2-48ae-bcd8-636004366f34/incentive-api-access']]
      ['https://graph.microsoft.com/v1.0/me', ['user.read']]
      //protectedResourceMap
    ]
  })
  ],
  declarations: [				
    AppComponent,
    // AdminLayoutComponent,
      LoginComponent,
      LogoutComponent
   ],
  providers: [AuthService, SpinnerService, ModelServiceService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
  MsalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
