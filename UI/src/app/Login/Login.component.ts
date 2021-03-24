import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FBMUsers } from 'app/model/FBMUsers';
import {AuthService} from '../services/auth.service';
import { SpinnerService} from '../services/spinner.service';
import { ModelServiceService } from '../services/ModelService.service';
import { MasterService } from 'app/services/master.service';

import { MsalService } from '@azure/msal-angular';
//import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { PayrollDateRequest } from 'app/model/PayrollDateRequest';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {
  payrollDateRequest : PayrollDateRequest;
  IncentiveAppCalendar : IncentiveAppCalendar;
  constructor(private router: Router, private authService : AuthService, private route: ActivatedRoute,
    public spinner : SpinnerService, private modelService : ModelServiceService, 
    private masterService : MasterService, private msalService: MsalService) 
    { 
      this.payrollDateRequest = new PayrollDateRequest;
      this.IncentiveAppCalendar = new IncentiveAppCalendar;
    }

  color = 'primary';
  mode = 'indeterminate';
  value = 50;
  bufferValue = 75;
  url: string;
  isNativeLogin : false;
  ngOnInit() {
    debugger;
    //alert(1);
    this.getPayrollDateDetails();
    this.url = this.router.url;
    const account = this.msalService.getAccount();

    // const renewIdTokenRequest = {​​​​ scopes: ["998ea47f-0d49-4928-b767-f5e80edd53ac"]}​​​​​;
    // var accessToken = null;
    // this.msalService.acquireTokenSilent(renewIdTokenRequest).then(function (tokenResponse) {
    //     // Callback code here
    //     debugger;
    //     console.log(tokenResponse.accessToken);
    //     accessToken = tokenResponse.accessToken;
    //     alert('IE Token -> ' + accessToken);

    // }).catch(function (error) {
    //     debugger;
    //     console.log(error);
    // });
    

    if(account)
    {
      if(account.idTokenClaims.preferred_username)
      {
        this.onADLogin(account.idTokenClaims.preferred_username);
      }
    }
  }

  onADLogin(userEmail)
  {
    //this.spinner.showSpinner(); 
    const token = this.authService.validateADUser(userEmail).subscribe(
      (data) => { 
        debugger;
        this.spinner.stopSpinner();

        //console.log(data)
        if(data){
          if(data.length > 0)
          {
            this.modelService.addItem(data[0]);
            if(this.route.snapshot.params['weekid']){
              this.spinner.stopSpinner();
              this.router.navigate(['/input-form/' + this.route.snapshot.params['weekid']
              + '/' + this.route.snapshot.params['formheaderid'] ]);
            }
            else{
              if(data[0].roleId == 3)
              {
                this.router.navigate(['/payroll-review']);
              }
              else{
                this.router.navigate(['/home']);
              }
            }
          }
          else{
            //Swal.fire({icon : 'error', title : 'Login Failed',text : 'Invalid Username/Password'});

            Swal.fire({
               title: 'Login Failed',
               text: "User is not exist in the system, please contact your system administrator",
               icon: 'warning',
               confirmButtonColor: '#103355',
               confirmButtonText: 'OK'
             }).then((result) => {
               if (result.isConfirmed) {
                localStorage.clear();  
                sessionStorage.clear();
                this.msalService.logout();  
               }
             })
          }
        }
        else{
          //Swal.fire({icon : 'error', title : 'Login Failed',text : 'Invalid Username/Password'});
          Swal.fire({
            title: 'Login Failed',
            text: "User is not exist in the system, please contact your system administrator",
            icon: 'warning',
            confirmButtonColor: '#103355',
            confirmButtonText: 'OK'
          }).then((result) => {
            if (result.isConfirmed) {
             localStorage.clear();  
             sessionStorage.clear();
             this.msalService.logout();  
            }
          })
        }
      }
    );
  }

  onLoginClick()
  {
    this.router.navigate(['/login']);
  }
  
  onLogin(loginForm: NgForm) {
    //console.log(loginForm.value);
    debugger;
    this.spinner.showSpinner(); 
    const token = this.authService.validateUser(loginForm.value).subscribe(
      (data) => { 
        debugger;
        this.spinner.stopSpinner();
        //console.log(data)
        if(data){
          if(data.length > 0)
          {
            this.modelService.addItem(data[0]);
            if(this.route.snapshot.params['weekid']){
              this.spinner.stopSpinner();
              this.router.navigate(['/input-form/' + this.route.snapshot.params['weekid']
              + '/' + this.route.snapshot.params['formheaderid'] ]);
            }
            else{
              if(data[0].roleId == 3)
              {
                this.router.navigate(['/payroll-review']);
              }
              else{
                this.router.navigate(['/home']);
              }
            }
          }
          else{
            Swal.fire({icon : 'error', title : 'Login Failed',text : 'Invalid Username/Password'});
          }
        }
        else{
          Swal.fire({icon : 'error', title : 'Login Failed',text : 'Invalid Username/Password'});
        }
      }
    );
  }

  getPayrollDateDetails(){
    this.masterService.getPayrollDateDetails(this.payrollDateRequest).subscribe(data => {
      debugger;
      if(data){
        debugger;
        this.IncentiveAppCalendar = data;
        this.modelService.addIncentiveAppCalendar(data);
      }
    });
  }

  onAzureADLoginClick()
  {
    this.router.navigate(['/ADLogin']);
  }
}
