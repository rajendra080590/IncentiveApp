import { Component, OnInit, Output, EventEmitter, ElementRef } from '@angular/core';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import { Router } from '@angular/router';
import { ModelServiceService } from 'app/services/ModelService.service';
import { Userdata } from 'app/model/userdata';
import { MatSidenav } from '@angular/material/sidenav';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();
  private listTitles: any[];
  location: Location;
    mobile_menu_visible: any = 0;
  private toggleButton: any;
  private sidebarVisible: boolean;
  public userDetails : Userdata;
  constructor(location: Location,  private element: ElementRef, private router: Router,
    private modelService : ModelServiceService, private msalService: MsalService) {
    debugger;
      this.location = location;
      this.sidebarVisible = true;
      this.userDetails = this.modelService.getItems();
  }

  ngOnInit() { }

  toggleSideBar() {
    this.toggleSideBarForMe.emit();
    setTimeout(() => {
      window.dispatchEvent(
        new Event('resize')
      );
    }, 300);
  }

  logOutClick()
    {
        debugger;
        localStorage.clear();  
        sessionStorage.clear();
        this.msalService.logout();  
        //this.router.navigate(['/login']);
    }

}
