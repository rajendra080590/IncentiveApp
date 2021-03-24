import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy, PopStateEvent } from '@angular/common';
import 'rxjs/add/operator/filter';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import PerfectScrollbar from 'perfect-scrollbar';
import * as $ from "jquery";
import { SpinnerService } from '../../services/spinner.service';
import { SidenavService } from '../../services/sidenav.service';
import { onMainContentChange } from '../../animations/animations';
import { ModelServiceService } from 'app/services/ModelService.service';
import { Userdata } from 'app/model/userdata';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {

  private _router: Subscription;
  private lastPoppedUrl: string;
  private yScrollStack: number[] = [];
  public onSideNavChange: boolean;
  sideBarOpen = true;
  public userDetails : Userdata;

  sideBarToggler() {
    this.sideBarOpen = !this.sideBarOpen;
  }

  constructor( public location: Location, private router: Router, 
    private _sidenavService: SidenavService,
    private modelService : ModelServiceService, private msalService: MsalService) {
      debugger;
      this.userDetails = this.modelService.getItems();

      this._sidenavService.sideNavState$.subscribe( res => {
        //console.log(res)
        this.onSideNavChange = res;
      })
  }

  ngOnInit() {
    if(!this.userDetails.UserId)
    {
      this.router.navigate(['/login']);
    }
    debugger;
    const account = this.msalService.getAccount();
    if(!account)
    {
      this.router.navigate(['/login']);
    }
  }
  ngAfterViewInit() {
     // this.runOnRouteChange();
  }
  isMaps(path){
      var titlee = this.location.prepareExternalUrl(this.location.path());
      titlee = titlee.slice( 1 );
      if(path == titlee){
          return false;
      }
      else {
          return true;
      }
  }
  runOnRouteChange(): void {
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      const elemMainPanel = <HTMLElement>document.querySelector('.main-panel');
      const ps = new PerfectScrollbar(elemMainPanel);
      ps.update();
    }
  }
  isMac(): boolean {
      let bool = false;
      if (navigator.platform.toUpperCase().indexOf('MAC') >= 0 || navigator.platform.toUpperCase().indexOf('IPAD') >= 0) {
          bool = true;
      }
      return bool;
  }

}

