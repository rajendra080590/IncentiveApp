import { Component, OnInit } from '@angular/core';
import { Iincentivedetails } from '../model/iincentivedetails';
import { SpinnerService } from '../services/spinner.service';
import { MasterService } from '../services/master.service';
import { Userdata } from 'app/model/userdata';
import { ModelServiceService } from 'app/services/ModelService.service';
import { DashboardDetails } from 'app/model/DashboardDetails';
import { DashboardService } from 'app/services/dashboard.service';
import { DashboardRequest } from 'app/model/DashboardRequest';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { ReviewerAction } from 'app/model/ReviewerAction';
import {MatDialog} from '@angular/material/dialog';
import { AudittraildialogComponent } from './audittraildialog/audittraildialog.component';
import { DeleteObjArray } from 'app/model/DeleteObjArray';
import { AuthService } from 'app/services/auth.service';
import { FBMUsers } from 'app/model/FBMUsers';
import { ReviewerActionRequest } from 'app/model/ReviewerActionRequest';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  reviewId : 1;
  statusId : 1;
  IncentiveDetails : Iincentivedetails [];
  dtOptions: DataTables.Settings = {};
  dtApprovedOptions: DataTables.Settings = {};
  public userDetails : Userdata;
  dashboardDetails : DashboardDetails[] = [];
  approvedDetails : DashboardDetails [] = [];
  rejectedDetails : DashboardDetails [] = [];
  draftDetails : DashboardDetails [] = [];
  deleteInputFormArray : DeleteObjArray [] = [];
  deleteRequestObj : DeleteObjArray;
  dashboradRequest : DashboardRequest;
  actionArray : ReviewerAction[] = [];
  actionObj : ReviewerActionRequest;
  selectedTabOption;

  constructor(public spinner : SpinnerService, private masterService : MasterService, private router: Router,
    private modelService : ModelServiceService, private dashboardService : DashboardService,
    public dialog: MatDialog, private route: ActivatedRoute, private authService : AuthService, ) { 
    //debugger
    this.spinner.showSpinner(); 
    this.userDetails = this.modelService.getItems();
    this.dashboradRequest = new DashboardRequest;
    this.deleteRequestObj = new DeleteObjArray;
    this.actionObj = new  ReviewerActionRequest;
    
    
    this.statusId = 1;
    //this.inProgressDetails = new DashboardDetails;
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 25],
      processing: true,
      ordering : false,
      columnDefs: [
        { "orderable": true, "targets": 1 }
      ]
    };
    this.dtApprovedOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 25],
      processing: true,
      ordering : false,
      columnDefs: [
        { "orderable": true, "targets": 1 }
      ]
    };
  }
  
  ngOnInit() {

    
    if(this.route.snapshot.params['userid'])
    {
      //debugger
      //this.spinner.showSpinner(); 
      const userid : number = parseInt(this.route.snapshot.params['userid']);
      const token = this.authService.validateUserId(userid).subscribe(
        (data) => { 
          //debugger
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
                this.getDashboardDetails(1);
              }
            }
            else{
              this.router.navigate(['/login']);
            }
          }
        }
      );
    }
    else{
      this.getDashboardDetails(1);
    }
  }
  
  getDashboardDetails(tabOption){
    //debugger
    //request : DashboardRequest;
    this.dashboradRequest.UserId = this.userDetails.UserId;
    this.dashboradRequest.RoleId = this.userDetails.RoleId;
    this.dashboradRequest.Status = tabOption;
    this.dashboardDetails = [];
    
    this.dashboardService.getDashboardDetails(this.dashboradRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.dashboardDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

  getApprovedDetails(tabOption){
    //debugger
    //request : DashboardRequest;
    this.dashboradRequest.UserId = this.userDetails.UserId;
    this.dashboradRequest.RoleId = this.userDetails.RoleId;
    this.dashboradRequest.Status = tabOption;
    this.approvedDetails = [];

    this.dashboardService.getDashboardDetails(this.dashboradRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.approvedDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

  getRejectionDetails(tabOption){
    //debugger
    //request : DashboardRequest;
    this.dashboradRequest.UserId = this.userDetails.UserId;
    this.dashboradRequest.RoleId = this.userDetails.RoleId;
    this.dashboradRequest.Status = tabOption;
    this.rejectedDetails = [];
    this.dashboardService.getDashboardDetails(this.dashboradRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.rejectedDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

  getDraftDetails(tabOption){
    //debugger
    //request : DashboardRequest;
    this.dashboradRequest.UserId = this.userDetails.UserId;
    this.dashboradRequest.RoleId = this.userDetails.RoleId;
    this.dashboradRequest.Status = tabOption;
    this.draftDetails = [];
    this.dashboardService.getDashboardDetails(this.dashboradRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.draftDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

  activeTab = '1';
  onTabClick(tabOption){
    this.spinner.showSpinner(); 
    this.statusId = tabOption;

    this.activeTab = tabOption;
    if(tabOption == 5){
      this.getApprovedDetails(tabOption);
    }
    else if(tabOption == 3){
      this.getRejectionDetails(tabOption);
    }
    else if(tabOption == 4)
    {
      this.getDraftDetails(tabOption);
    }
    else
      this.getDashboardDetails(tabOption);
  }

  onActionClick(option, obj){
    //debugger
    this.actionObj.formHeaderId = obj.formHeaderId;
    this.actionObj.userId = this.userDetails.UserId;
    this.actionObj.roleId = this.userDetails.RoleId;
    this.actionObj.branchId = this.userDetails.BranchId;
    this.actionObj.submittedBy = obj.submittedBy;
    this.actionObj.submitterEmail = obj.submitterEmail;
    this.actionObj.weekId = obj.weekId;
    this.actionObj.createdBy = obj.createdBy;
    this.actionArray = [];
    if(option == 2)
    {
      Swal.fire({
       // title: 'Are you sure?',
       // text: "You want to approve this!",
        icon: 'warning',
        input: 'text',
        inputPlaceholder: "Add your comments here",
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Approve!'
      }).then((result) => {

        if (result.isConfirmed) {
          this.actionObj.statusId = 2;
          this.actionObj.comments = result.value;
          if(this.userDetails.RoleId == 3){
            this.actionObj.statusId = 5;
          }
          this.actionArray.push(this.actionObj);
          this.spinner.showSpinner(); 
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.getDashboardDetails(1);
              Swal.fire(
                'Approved!',
                'Your have approved the incentive form.',
                'success'
              )
            }
          });
        }
      })
    }
    else{
      Swal.fire({
        //title: 'Are you sure?',
        //text: "You want to reject this!",
        icon: 'warning',
        input: 'text',
        //html : "<div><p>You want to reject this!</p><p><input id='swal-input2' class='swal2-input' required/></p></div>",
        inputPlaceholder: "Add your comments here",
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Reject!',
        preConfirm: function (comment) {
            if (comment === '') {
              Swal.showValidationMessage('Please input comment for rejection');
            }
        }
      }).then((result) => {
        //debugger
        if (result.isConfirmed) {
          this.actionObj.statusId = 3;
          this.actionObj.comments = result.value;
          this.actionArray.push(this.actionObj);
          this.spinner.showSpinner(); 
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.getDashboardDetails(1);
              Swal.fire(
                'Rejected!',
                'Your have rejected the incentive form.',
                'success'
              )
            }
          });
          
        }
      })
    }
  }

  onDraftSubmitClick(draftObj){
    this.actionObj.formHeaderId = draftObj.formHeaderId;
    this.actionObj.userId = this.userDetails.UserId;
    this.actionObj.roleId = this.userDetails.RoleId;
    this.actionObj.branchId = this.userDetails.BranchId;
    this.actionArray = [];
    Swal.fire({
      //title: 'Are you sure?',
      //text: "You want to submit this!",
      
      input: 'text',
      inputPlaceholder: "Add your comments here",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#103355',
      cancelButtonColor: '#7A1316',
      confirmButtonText: 'Yes, Submit!'
    }).then((result) => {

      if (result.isConfirmed) {

        this.actionObj.statusId = 1;
        this.actionArray.push(this.actionObj);
        this.spinner.showSpinner(); 
        this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
          //debugger
          if(data){
            //debugger
            this.spinner.stopSpinner(); 
            this.actionArray = [];
            this.getDraftDetails(4);
            Swal.fire(
              'Submitted!',
              'Your have submitted the incentive form.',
              'success'
            )
          }
        });
      }
    })
  }
  
  onDraftSubmitAllClick(){
    //debugger
    if(this.actionArray.length > 0)
    {
      Swal.fire({
        //title: 'Are you sure?',
        //text: "You want to submit this!",
        input: 'text',
        inputPlaceholder: "Add your comments here",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Submit!'
      }).then((result) => {
  
        if (result.isConfirmed) {
          this.spinner.showSpinner(); 
          for (let i = 0; i < this.actionArray.length; i++) {
            this.actionArray[i].statusId = 1;
            this.actionArray[i].comments = result.value;
          }
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.actionArray = [];
              this.getDraftDetails(4);
              Swal.fire(
                'Submitted!',
                'Your have submitted the incentive form.',
                'success'
              )
            }
          });
        }
      })
    }
    else{
      Swal.fire('Please do form selection for submition')
      return false;
    }
  }
  onDetailsClick(weekId, formHederId){
    this.router.navigate(['/input-form/' + weekId + '/' + formHederId ]);
  }
  onInProgressDetailsClick(weekId, formHederId, createBy){
    this.router.navigate(['/input-form/' + weekId + '/' + formHederId + '/' + createBy ]);
  }
  onRejectedDetailsClick(weekId, formHederId, createBy, statusId){
    this.router.navigate(['/input-form/' + weekId + '/' + formHederId + '/' + createBy + '/' + statusId]);
  }
  onDraftDetailsClick(weekId, formHederId, statusId){
    this.router.navigate(['/input-form/' + weekId + '/' + formHederId + '/' + this.userDetails.UserId + '/'  + statusId]);
  }

  onDeleteClick(deleteObj){
    this.deleteInputFormArray = [];
    Swal.fire({
      //title: 'Are you sure?',
      //text: "You want to delete this!",
      icon: 'warning',
      input: 'text',
      inputPlaceholder: "Add your comments here",
      showCancelButton: true,
      confirmButtonColor: '#103355',
      cancelButtonColor: '#7A1316',
      confirmButtonText: 'Yes, Delete!'
    }).then((result) => {
      if (result.isConfirmed) {
        //debugger
        this.deleteInputFormArray.push(deleteObj);
        this.dashboardService.deleteInputForm(this.deleteInputFormArray).subscribe(data => {
          //debugger
          if(data){
            //debugger
            this.spinner.stopSpinner(); 
            this.getDraftDetails(4);
            Swal.fire(
              'Deleted!',
              'Your have deleted the incentive form.',
              'success'
            )
          }
        });
      }
    })
  }

  onCheckBoxSelection(obj, isChecked){
    //debugger
    this.actionObj = new ReviewerActionRequest;
    this.deleteRequestObj = new DeleteObjArray;
    if(isChecked)
    {
      this.actionObj.formHeaderId = obj.formHeaderId;
      this.actionObj.userId = this.userDetails.UserId;
      this.actionObj.roleId = this.userDetails.RoleId;
      this.actionObj.branchId = this.userDetails.BranchId;
      this.actionObj.submittedBy = obj.submittedBy;
      this.actionObj.submitterEmail = obj.submitterEmail;
      this.actionObj.weekId = obj.weekId;
      this.actionObj.createdBy = obj.createdBy;

      this.deleteRequestObj.formHeaderId = obj.formHeaderId;
      this.deleteRequestObj.option = 0;
      if (!this.actionArray.find((x) => x.formHeaderId === obj.formHeaderId)) {
        this.actionArray.push(this.actionObj);
      }
      if (!this.deleteInputFormArray.find((x) => x.formHeaderId === obj.formHeaderId)) {
        this.deleteInputFormArray.push(this.deleteRequestObj);
      }
    }
    else{
      for( let i = 0; i < this.actionArray.length; i++){ 
        if (this.actionArray[i].formHeaderId == obj.formHeaderId) { 
          this.actionArray.splice(i, 1); 
        }
      }
      for( let i = 0; i < this.deleteInputFormArray.length; i++){ 
        if (this.deleteInputFormArray[i].formHeaderId == obj.formHeaderId) { 
          this.deleteInputFormArray.splice(i, 1); 
        }
      }
    }
  }
  checkAll(ev) {
    //debugger
    //this.dashboardDetails.forEach(x => x.isSelected  = ev.target.checked)
    this.actionArray = [];
    this.dashboardDetails.forEach((x) => {
      this.actionObj = new ReviewerAction;
      if(this.userDetails.RoleId == 2)
      {
        if(x.statusId == 1){
          x.isSelected = ev.target.checked;
          if(ev.target.checked)
          {
            this.actionObj.formHeaderId = x.formHeaderId;
            this.actionObj.userId = this.userDetails.UserId;
            this.actionObj.roleId = this.userDetails.RoleId;
            this.actionObj.branchId = this.userDetails.BranchId;
            this.actionObj.submittedBy = x.submittedBy;
            this.actionObj.submitterEmail = x.submitterEmail;
            this.actionObj.weekId = x.weekId;
            this.actionObj.createdBy = x.createdBy;
            this.actionArray.push(this.actionObj);
          }
        }
      }

      if(this.userDetails.RoleId == 3)
      {
        if(x.statusId == 2){
          x.isSelected = ev.target.checked;
          if(ev.target.checked)
          {
            this.actionObj.formHeaderId = x.formHeaderId;
            this.actionObj.userId = this.userDetails.UserId;
            this.actionObj.roleId = this.userDetails.RoleId;
            this.actionObj.branchId = this.userDetails.BranchId;
            this.actionObj.submittedBy = x.submittedBy;
            this.actionObj.submitterEmail = x.submitterEmail;
            this.actionObj.weekId = x.weekId;
            this.actionObj.createdBy = x.createdBy;
            this.actionArray.push(this.actionObj);
          }
        }
      }
    });
  }
  isAllChecked() {
    return this.dashboardDetails.every(_ => _.isSelected);
  }

  isAllDraftChecked() {
    return this.draftDetails.every(_ => _.isSelected);
  }

  onApproveAllClick(approverRoleId){
    if(this.actionArray.length > 0)
    {
      Swal.fire({
        //title: 'Are you sure?',
        //text: "You want to approve this!",
        icon: 'warning',
        input: 'text',
        inputPlaceholder: "Add your comments here",
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Approve!'
      }).then((result) => {
        if (result.isConfirmed) {
          if(this.userDetails.RoleId == 3){
            for (let i = 0; i < this.actionArray.length; i++) {
              this.actionArray[i].statusId = 5;
              this.actionArray[i].comments = result.value;
            }
          }
          else if(this.userDetails.RoleId == 2)
          {
            for (let i = 0; i < this.actionArray.length; i++) {
              this.actionArray[i].statusId = 2;
              this.actionArray[i].comments = result.value;
            }
          }
          
          this.spinner.showSpinner(); 
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.getDashboardDetails(1);
              Swal.fire(
                'Approved!',
                'Your have approved the incentive forms.',
                'success'
              )
            }
          });
        }
      })
    }
    else{
      Swal.fire('Please do form selection for approval')
      return false;
    }
  }
  onRejectAllClick(rejectorRoleId){
    if(this.actionArray.length > 0)
    {
      Swal.fire({
        //title: 'Are you sure?',
        //text: "You want to reject this!",
        icon: 'warning',
        input: 'text',
        inputPlaceholder: "Add your comments here",
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Reject!',
        preConfirm: function (comment) {
          if (comment === '') {
            Swal.showValidationMessage('Please input comment for rejection');
          }
      }
      }).then((result) => {
        //debugger
        if (result.isConfirmed) {
          for (let i = 0; i < this.actionArray.length; i++) {
            this.actionArray[i].statusId = 3;
            this.actionArray[i].comments = result.value;
          }
          this.spinner.showSpinner(); 
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.getDashboardDetails(1);
              Swal.fire(
                'Rejected!',
                'Your have rejected the incentive form.',
                'success'
              )
            }
          });
          
        }
      })
    }
    else{
      Swal.fire('Please do form selection for rejection')
      return false;
    }
  }

  onDeleteAllClick(){
    if(this.deleteInputFormArray.length > 0)
    {
      Swal.fire({
        //title: 'Are you sure?',
        //text: "You want to reject this!",
        icon: 'warning',
        input: 'text',
        inputPlaceholder: "Add your comments here",
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes, Delete!'
      }).then((result) => {
        //debugger
        if (result.isConfirmed) {
          this.spinner.showSpinner(); 
          this.dashboardService.deleteInputForm(this.deleteInputFormArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.getDraftDetails(4);
              Swal.fire(
                'Deleted!',
                'Your have deleted the incentive form.',
                'success'
              )
            }
          });
          
        }
      })
    }
    else{
      Swal.fire('Please do form selection for deletion')
      return false;
    }
  }
  

  openDialog(detailsObj): void {
    //debugger
    const dialogRef = this.dialog.open(AudittraildialogComponent, {
      width: '100%', height : '80%',
      data: detailsObj
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The dialog was closed');
      //this.animal = result;
    });
  }

  
}

export interface DialogData {
  statusId : number;
  branchId : string;
  roleId : string;
  formHeaderId :  string;
}