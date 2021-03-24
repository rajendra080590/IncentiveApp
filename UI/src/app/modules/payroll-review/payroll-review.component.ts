import { Component, Input, OnInit } from '@angular/core';
import { SpinnerService } from '../../services/spinner.service';
import { MasterService } from '../../services/master.service';
import { Userdata } from 'app/model/userdata';
import { ModelServiceService } from 'app/services/ModelService.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { DashboardService } from 'app/services/dashboard.service';
import { PayrollPending } from 'app/model/PayrollPending';
//import { saveAs } from 'file-saver/FileSaver';
import * as fileSaver from 'file-saver';
import { PayrollDownload } from 'app/model/PayrollDownload';
import { TitleCasePipe } from '../../Pipes/title-case.pipe';
import { BranchMaster } from 'app/model/BranchMaster';
import { PayGroup } from 'app/model/PayGroup';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { PayrollService } from 'app/services/payroll.service';
import { PayrollReview } from 'app/model/PayrollReview';
import { PayrollReviewRequest } from 'app/model/payrollReviewRequest';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { Regions } from 'app/model/Regions';
import { States } from 'app/model/States';
import { ReviewerAction } from 'app/model/ReviewerAction';
import { MatDialog } from '@angular/material/dialog';
import { AudittraildialogComponent } from 'app/dashboard/audittraildialog/audittraildialog.component';
import { PayrollReviewed } from 'app/model/PayrollReviewed';

const file_Extention = '.csv';

@Component({
  selector: 'app-payroll-review',
  templateUrl: './payroll-review.component.html',
  styleUrls: ['./payroll-review.component.scss']
})
export class PayrollReviewComponent implements OnInit {

  payrollReviewDetails : PayrollReview[] = [];
  payrollReviewRequest : PayrollReviewRequest;
  public payrollReviewedArray : PayrollReviewed[] = [];
  payrollReviewedObj : PayrollReviewed;
  dtOptions: any = {};
  public userDetails : Userdata;
  public branchMasterArray : BranchMaster[];
  public payGroupArray : PayGroup[];
  public regionsArray : Regions[];
  public statesArray : States[];
  public weekIdArray : IncentiveAppCalendar[];
  actionArray : ReviewerAction[] = [];
  actionObj : ReviewerAction;
  
  branchList = new FormControl();
  payGroupName = new FormControl(); isAllFormDisable : boolean = false;
  weekDate : Date; weekNumber : string = null;weekId : string;  payrollDate : Date;
  totalBranches : number = 73; totalAmount : number = 0; totalSubmittedForm : number = 0;
  totalNotSubmittedBranches : number = 0;
  totalBranchApproved : number = 0; totalPayrollApproved : number = 0;
  totalBranchPending : number = 0; totalPayrollPending : number = 0;
  regionCode : string = 'All';stateCode : string = 'All'; branchId : string = 'All'; payGroup : string = 'All';
  @Input()
  max: Date | null;
  today = new Date(); 
  incentiveAppCalendar : IncentiveAppCalendar;
  constructor(public spinner : SpinnerService, private masterService : MasterService, private router: Router,
    private modelService : ModelServiceService, private dashboardService : DashboardService,
    private titleCasePipe : TitleCasePipe, private payrollService : PayrollService,
    public dialog: MatDialog) { 
      this.userDetails = this.modelService.getItems();
      //this.weekNumber = this.getWeekNumber(this.weekDate);
      this.payrollReviewRequest = new PayrollReviewRequest;
      this.payrollReviewedObj = new PayrollReviewed;
      this.actionObj = new  ReviewerAction;
      this.incentiveAppCalendar = this.modelService.getIncentiveAppCalendar();
      this.weekId = this.incentiveAppCalendar.weekId;
      this.payrollDate = this.incentiveAppCalendar.payrollDate;
      this.today.setDate(this.today.getDate());
      this.dtOptions = {
        pagingType: 'full_numbers',
        pageLength: 15,
        lengthMenu : [5,10,15,25,50,75,100],
        processing: true,
        ordering : true,
        columnDefs: [
          { "orderable": false, "targets": [0,1] }
        ]
      };
    }

  ngOnInit() {
    this.spinner.showSpinner(); 
    this.getAllBranches(0, 0);
    this.getAllPayGroups();
    this.getAllWeekIdDetails();
    this.getAllRegions(0,null);
    this.getAllStates(0,0);
    this.getPayrollReviewDetails();
  }


  getPayrollReviewDetails(){
    //debugger;
    this.payrollReviewRequest.weekId = this.weekId;
    this.payrollReviewRequest.option = 0;
    this.actionArray = [];
    this.payrollReviewedArray = [];
    this.payrollService.getPayrollReviewDetails(this.payrollReviewRequest).subscribe(data => {
      debugger;
      if(data){
        //debugger;
        this.payrollReviewDetails = [];
        this.payrollReviewDetails = data;
        for(let i=0; i < this.payrollReviewDetails.length; i++ )
        {
          if(this.payrollReviewDetails[i].statusId == 5)
          {
            this.payrollReviewDetails[i].isSelected = true;
            this.payrollReviewDetails[i].isReviewed = true;
          }
        }
        this.getSubmittedDetails(data)
        this.spinner.stopSpinner(); 
      }
    });
  }

  getSubmittedDetails(data)
  {
    //debugger;
    const submittedArray = data.filter(x => x.statusId == 1);
    const branchApprovedArray = data.filter(x => x.statusId == 2);
    const payrollApprovedArray = data.filter(x => x.statusId == 5);
    const totalSubmittedArray = data.filter(x => x.weekId != null);
    const distinctSubmittedBranches = totalSubmittedArray.filter(
      (thing, i, arr) => arr.findIndex(t => t.branchId === thing.branchId) === i
    );
    this.totalNotSubmittedBranches = this.totalBranches - distinctSubmittedBranches.length;
    this.totalBranchPending = submittedArray.length;
    this.totalPayrollPending = branchApprovedArray.length;
    this.totalAmount = 0;
    this.totalSubmittedForm = totalSubmittedArray.length;
    this.isAllFormDisable = false;
    if(branchApprovedArray.length == 0)
    {
      //this.isAllFormDisable = true;
    }
    this.totalBranchApproved = branchApprovedArray.length;
    this.totalPayrollApproved = payrollApprovedArray.length;
    for(let j=0;j< totalSubmittedArray.length;j++){  
      this.totalAmount+= parseFloat(totalSubmittedArray[j].totalAmount);  
    }  
  }
  
  searchOnClick(){
    //debugger;
    this.spinner.showSpinner(); 
    this.payrollReviewDetails = [];
    this.payrollReviewRequest.weekId = this.weekId;
    this.payrollReviewRequest.regionCode = this.regionCode == 'All' ? null : parseInt(this.regionCode);
    this.payrollReviewRequest.stateCode = this.stateCode == 'All' ? null : this.stateCode;
    this.payrollReviewRequest.branchId = this.branchId == 'All' ? null : this.branchId;
    this.payrollReviewRequest.payGroup = this.payGroup == 'All' ? null : this.payGroup;
    this.payrollReviewRequest.option = 1;
    if(this.regionCode == 'All' && this.stateCode == 'All' && this.branchId == 'All' && this.payGroup == 'All')
    {
      this.payrollReviewRequest.option = 0;
    }
    this.payrollService.getPayrollReviewDetails(this.payrollReviewRequest).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        
        this.payrollReviewDetails = data;
        for(let i=0; i < this.payrollReviewDetails.length; i++ )
        {
          if(this.payrollReviewDetails[i].statusId == 5)
          {
            this.payrollReviewDetails[i].isSelected = true;
            this.payrollReviewDetails[i].isReviewed = true;
          }
        }
        this.getSubmittedDetails(data)
        this.spinner.stopSpinner(); 
      }
    });
  }

  clearClick()
  {
    this.spinner.showSpinner(); 
    this.branchList.setValue("");
    this.weekNumber = null;
    this.weekDate = null;
    this.payGroupName.setValue("");
  }

  getAllBranches(option, regionCode){
    this.masterService.getAllBranches(option, regionCode).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.branchMasterArray = data;  
        if(option == 0)
        {
          this.totalBranches = data.length;
        }
      }
    });
  }

  getAllPayGroups(){
    this.masterService.getAllPayGroups().subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.payGroupArray = data;
      }
    });
  }

  getAllRegions(option, stateCode){
    this.masterService.getAllRegions(0, stateCode).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.regionsArray = data;
      }
    });
  }

  getAllStates(option, regionCode){
    this.masterService.getAllStates(option, regionCode).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.statesArray = data;
      }
    });
  }

  getAllWeekIdDetails(){
    this.masterService.getAllWeekIdDetails().subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.weekIdArray = data;
      }
    });
  }

  weekNumberChange($event)
  {
    //debugger;
    this.payrollDate = this.weekIdArray.filter(x => x.weekId == $event.value)[0].payrollDate;
  }

  onSelectionChanged(optionType, $event)
  {
    //debugger;
    if(optionType == 'Branch')
    {
      this.branchId = $event.value;
    }
    else if(optionType == 'Region')
    {
      this.regionCode = $event.value;
      if($event.value == 'All'){
        this.getAllStates(0,0);
      }
      else{
        this.getAllStates(1,this.regionCode);
      }
    }
    else if(optionType == 'State')
    {
      this.stateCode = $event.value;
      if($event.value == 'All'){
        this.getAllBranches(0,this.stateCode);
      }
      else{
        this.getAllBranches(1,this.stateCode);
      }
    }
    else if(optionType == 'PayGroup')
    {
      this.payGroup = $event.value;
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
        //debugger;
        if (result.isConfirmed) {
          for (let i = 0; i < this.actionArray.length; i++) {
            this.actionArray[i].statusId = 3;
            this.actionArray[i].comments = result.value;
          }
          this.spinner.showSpinner(); 
          this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
            //debugger;
            if(data){
              //debugger;
              this.getPayrollReviewDetails();
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

  onApproveAllClick(approverRoleId){
    if(this.actionArray.length > 0 || this.payrollReviewedArray.length > 0)
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

          this.spinner.showSpinner();
          if(this.payrollReviewedArray.length > 0)
          {
            for (let i = 0; i < this.payrollReviewedArray.length; i++) {
              this.payrollReviewedArray[i].statusId = 5;
              this.payrollReviewedArray[i].comments = result.value;
            }

            this.payrollService.payrollReviewedAction(this.payrollReviewedArray).subscribe(data => {
              //debugger;
              if(data){
                //debugger;
                if(this.actionArray.length == 0)
                {
                  this.spinner.stopSpinner(); 
                  this.getPayrollReviewDetails();
                  Swal.fire(
                    'Approved!',
                    'Your have approved the incentive forms.',
                    'success'
                  )
                }
              }
            });
          }

          if(this.actionArray.length > 0)
          {
            for (let i = 0; i < this.actionArray.length; i++) {
              this.actionArray[i].statusId = 5;
              this.actionArray[i].comments = result.value;
            }
            this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
              //debugger;
              if(data){
                //debugger;
                this.spinner.stopSpinner(); 
                this.getPayrollReviewDetails();
                Swal.fire(
                  'Approved!',
                  'Your have approved the incentive forms.',
                  'success'
                )
              }
            });
          }
        }
      })
    }
    else{
      Swal.fire('Please do form selection for approval')
      return false;
    }
  }

  onCheckBoxSelection(obj, isChecked, $event){
    debugger;
    this.actionObj = new ReviewerAction;
    this.payrollReviewedObj= new PayrollReviewed;
    if(isChecked)
    {
      if(!obj.weekId){
        if(!obj.isReviewed){
          Swal.fire('Please review and Confirm that this Branch has no incentives for this week before it can be approved.', '', 'warning').
          then((result) => {
            obj.isSelected = false;
            $event.target.checked = false;
          });
          obj.isSelected = false;
          $event.target.checked = false;
          return false;
        }
        else
        {
          this.payrollReviewedObj.branchId = obj.branchId;
          this.payrollReviewedObj.reviewedBy = this.userDetails.FirstName;
          this.payrollReviewedObj.reviewedUserId = this.userDetails.UserId;
          this.payrollReviewedObj.submittedBy = this.userDetails.UserId;
          this.payrollReviewedObj.weekId = this.weekId;
          this.payrollReviewedObj.payrollDate = this.payrollDate;
          if (!this.payrollReviewedArray.find((x) => x.branchId === obj.branchId)) {
            this.payrollReviewedArray.push(this.payrollReviewedObj);
          }
          return false;
        }
      }
      this.actionObj.formHeaderId = obj.formHeaderId;
      this.actionObj.userId = this.userDetails.UserId;
      this.actionObj.roleId = this.userDetails.RoleId;
      this.actionObj.branchId = obj.branchId;
      this.actionObj.submittedBy = obj.submittedBy;
      this.actionObj.submitterEmail = obj.submitterEmail;
      this.actionObj.weekId = obj.weekId;
      this.actionObj.createdBy = obj.createdBy;
      if (!this.actionArray.find((x) => x.formHeaderId === obj.formHeaderId)) {
        this.actionArray.push(this.actionObj);
      }
    }
    else{
      for( let i = 0; i < this.actionArray.length; i++){ 
        if ( this.actionArray[i].branchId === obj.branchId) { 
          this.actionArray.splice(i, 1); 
        }
      }
      for( let i = 0; i < this.payrollReviewedArray.length; i++){ 
        if ( this.payrollReviewedArray[i].branchId === obj.branchId) { 
          this.payrollReviewedArray.splice(i, 1); 
        }
      }
    }
  }
  onReviewedCheckboxSelection(obj, isChecked)
  {
    if(isChecked){}
    else
    {
      for( let i = 0; i < this.payrollReviewedArray.length; i++){ 
        if ( this.payrollReviewedArray[i].branchId === obj.branchId) { 
          this.payrollReviewedArray.splice(i, 1); 
        }
      }
    }
  }

  customTrackBy(index: number, obj: PayrollReview): any {
    return index;
  }
  checkAll(ev) {
    //debugger;
    //this.dashboardDetails.forEach(x => x.isSelected  = ev.target.checked)
    this.actionArray = [];
    this.payrollReviewDetails.forEach((x) => {
      this.actionObj = new ReviewerAction;
      if(x.statusId == 5){
        return false;
      }
      if(x.statusId == 2){
        x.isSelected = ev.target.checked;
        if(ev.target.checked)
        {
          this.actionObj.formHeaderId = x.formHeaderId;
          this.actionObj.userId = this.userDetails.UserId;
          this.actionObj.roleId = this.userDetails.RoleId;
          this.actionObj.branchId = this.userDetails.BranchId;
          this.actionObj.submittedBy = x.submittedBy;
          //this.actionObj.submitterEmail = x.submitterEmail;
          this.actionObj.weekId = x.weekId;
          //this.actionObj.createdBy = x.submittedBy;
          this.actionArray.push(this.actionObj);
        }
      }
      else
      {
        //debugger;
        x.isSelected = false;
        if(x.isReviewed)
        {
          x.isSelected = ev.target.checked;
        }
      }
    });
    //console.log(this.payrollReviewDetails);
  }

  isAllChecked() {
    //return this.payrollReviewDetails.every(_ => _.isSelected);
  }

  onDetailsClick(weekId, formHederId){
    this.router.navigate(['/input-form/' + weekId + '/' + formHederId]);
  }

  openDialog(detailsObj): void {
    //debugger;
    const dialogRef = this.dialog.open(AudittraildialogComponent, {
      width: '100%', height : '80%',
      data: detailsObj
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The dialog was closed');
      //this.animal = result;
    });
  }
  currentWeekNo;
  public weekNuberArray : WeekModel[] = [];
  public weekObj = new WeekModel;
  getAllWeekNumber()
  {
    //debugger;
    this.currentWeekNo = moment(this.weekDate).week();
    
    for(let i= 0; i < 52; i++)
    {
      this.weekObj.weekId =  (this.weekDate.getFullYear() + 'W' +  (this.currentWeekNo - i)).toString(); 
       this.weekNuberArray.push(this.weekObj);
    }
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //debugger;
    //this.events.push(`${type}: ${event.value}`);
    this.weekNumber = this.getWeekNumber(event.value);
    
  }
  public getWeekNumber(d : Date) : string{
    return d.getFullYear() + 'W' +  moment(d).week();
    //return moment(d).week() - moment(d).startOf('month').week() + 1 + '' + moment(d).week() + '' + d.getFullYear();
  }

}

export class WeekModel {
  weekId : string;
}

