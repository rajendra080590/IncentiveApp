import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { INewFormBase } from '../model/inewformbase';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { MatPaginator } from "@angular/material/paginator";
import {  MatSort } from "@angular/material/sort";
import {   MatTableDataSource } from "@angular/material/table";
import { InputformService } from "../services/inputform.service";
import { MasterService } from 'app/services/master.service';
import { BranchMaster } from 'app/model/BranchMaster';
import { ModelServiceService } from 'app/services/ModelService.service';
import { Userdata } from 'app/model/userdata';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import * as moment from 'moment';
import { Inputform } from 'app/model/inputform';
import {CurrencyPipe, DatePipe} from '@angular/common';
import { SpinnerService } from 'app/services/spinner.service';
import { ActivatedRoute, Router } from '@angular/router';
import * as _ from 'underscore';
import { element } from 'protractor';
import { InputFormRequest } from 'app/model/InputFormRequest';
import { AuditTrail } from 'app/model/AuditTrail';
import { DashboardRequest } from 'app/model/DashboardRequest';
import { DashboardService } from 'app/services/dashboard.service';
import { DeleteObjArray } from 'app/model/DeleteObjArray';
import { ReviewerAction } from 'app/model/ReviewerAction';
import { PayrollDateRequest } from 'app/model/PayrollDateRequest';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { PayGroup } from 'app/model/PayGroup';
import { FileToUpload } from 'app/model/FileToUpload';
import { EmployeeDetails } from 'app/model/EmployeeDetails';
import { PrimaryJobs } from 'app/model/PrimaryJobs';
import { UploadDownloadService } from 'app/services/upload-download.service';
import { ProgressStatusEnum, ProgressStatus } from 'app/model/progress-status-model';
import { FileDetails } from 'app/model/FileDetails';

// Maximum file size allowed to be uploaded = 1MB
const MAX_SIZE: number = 1048576;

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  events: string[] = [];
  inputForm: FormGroup;
  @Input() text = 'Upload';
  /** Name used in form which will be sent in HTTP request. */
  @Input() param = 'file';
  @Input() accept = '.pdf,.doc,.word,.txt,.xlsx';
  @Output() complete = new EventEmitter<string>();
  pageTitle : string = 'New Input Form';
  form: FormGroup;

  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  fileUploadModelList : FileToUpload[] = [];
  public fileNames : string = null; 
  //public files: Array<FileUploadModel> = [];
  public files: FileDetails[];
  public fileInDownload: string;
  public percentage: number;
  public showProgress: boolean;
  public showDownloadError: boolean;
  public showUploadError: boolean;

  newFormDetails : INewFormBase[];
  currentItemsToShow : INewFormBase[];

  theFiles: any[] = [];
  messages: string[] = [];

  auditDtOptions: DataTables.Settings = {};
  auditTrailDetails : AuditTrail [] = [];
  deleteInputFormArray : DeleteObjArray [] = [];
  actionArray : ReviewerAction[] = [];
  actionObj : ReviewerAction;
  dashboradRequest : DashboardRequest;
  payrollDateRequest : PayrollDateRequest;
  incentiveAppCalendar : IncentiveAppCalendar;

  dataSource: MatTableDataSource<InputForm>;
  public inputFormArray: EmployeeDetails[] = [];
  public actualInputFormArray: EmployeeDetails[] = [];
  public branchMasterArray : BranchMaster[];
  public primaryJobsArray : PrimaryJobs[];
  inputFormDetails : Inputform[] = [];
  dtOptions: DataTables.Settings = {};
  inputFormRequest : InputFormRequest;
  selected; primaryJobSelected : string = 'All';
  branchName : string; payrollDate : Date; totalAmount : number = 0;
  public userDetails : Userdata;
  userId : number; requestCreatedBy : number; createdByForRejectEmail : number;
  branchId : string;
  weekId : string; currentWeekId : string = this.getWeekNumber(new Date());
  payrollWeekId : string;
  weekDate = new Date(); weekNumber : string; paramFormHeaderId : string; urlBranchId : string;
  paramStatusId : number; formHeaderCommnets : string;
  isFormDisable : boolean = false;isHeaderDisable : boolean = false;
  isDeleteOption : boolean = false;isApproveDisable : boolean = false;
  isRejectDisable : boolean = false;public payGroupArray : PayGroup[];
  isPrevWeekFormDisable : boolean = false;
  public weekIdArray : IncentiveAppCalendar[];
  isApprovedData = [];
  constructor(public router: Router, private fb: FormBuilder, private inputFormService : InputformService, 
    private masterService : MasterService, private modelService : ModelServiceService,
    public spinner : SpinnerService, private route: ActivatedRoute,private dashboardService : DashboardService,
    private uploadDownloadService: UploadDownloadService, private cp: CurrencyPipe) { 
      //debugger
      this.userDetails = this.modelService.getItems();
      this.weekNumber = this.getWeekNumber(this.weekDate);
      this.inputFormRequest = new InputFormRequest;
      this.dashboradRequest = new DashboardRequest;
      this.payrollDateRequest = new PayrollDateRequest;
      this.actionObj = new  ReviewerAction;
      this.incentiveAppCalendar = this.modelService.getIncentiveAppCalendar();
      this.weekId = this.incentiveAppCalendar.weekId;
      this.payrollDate = this.incentiveAppCalendar.payrollDate;
      //this.payrollWeekId = this.getWeekNumber(new Date(this.payrollDate));
  }
  
  ngOnInit() {
    debugger
    
    this.userId = this.userDetails.UserId;
    this.branchId = this.userDetails.BranchId;
    this.spinner.showSpinner();
    if(this.route.snapshot.params['weekid'])
    {
      this.weekId = this.route.snapshot.params['weekid'];
      this.isFormDisable = true;
      this.isHeaderDisable = true;
    }
    if(this.route.snapshot.params['userid'])
    {
      this.requestCreatedBy = this.route.snapshot.params['userid'];
    }
    

    this.inputFormRequest.branchId = this.userDetails.BranchId;
    if(this.route.snapshot.params['formheaderid'])
    {
      this.paramFormHeaderId = this.route.snapshot.params['formheaderid'];
      this.inputFormRequest.branchId = this.paramFormHeaderId.substring(0,6);
      this.urlBranchId = this.paramFormHeaderId.substring(0,6);
      this.branchId = this.urlBranchId;
      this.selected = this.paramFormHeaderId.substring(0,6);
      this.getAuditTrailDetails(this.paramFormHeaderId, this.urlBranchId);
      this.getFiles(this.paramFormHeaderId);
    }
    this.getAllWeekIdDetails();
    if(this.userDetails.RoleId == 3)
    {
      this.getAllBranches(0,0);
    }
    else{
      this.getAllUserBranches(this.userDetails.UserId);
    }
    if(this.route.snapshot.params['statusid']){
      this.paramStatusId = this.route.snapshot.params['statusid'];
    }

    if(this.route.snapshot.params['statusid'] != 4){
      this.inputFormRequest.formHeaderId = this.paramFormHeaderId;
    }
    
    //this.getInpuFormDetails(this.userDetails.BranchId, this.weekNumber)
    this.inputFormRequest.weekId = this.weekId;
    this.inputFormService.getInputFormDetails(this.inputFormRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.spinner.stopSpinner();
        //this.weekDate = data.filter(x => x.weekId == this.weekNumber)[0].createdDate;
        if(data.length > 0)
        {
          
          if(this.paramFormHeaderId)
          {
            this.createdByForRejectEmail = data[0].createdBy;
            //this.formHeaderCommnets = data[0].formHeaderComments;
            if((data[0].statusId == 2 && this.userDetails.RoleId == 2) || data[0].statusId == 3)
            {
              this.isApproveDisable = true;
              this.isRejectDisable = true;
            }
            if(data[0].statusId == 3)
            {
              this.isFormDisable = false;
              this.isHeaderDisable = true;
              this.isDeleteOption = true;
            }
            //check for draft data
            let isDraftData = data.filter(x => x.statusId == 4);
            let isRejectedData = data.filter(x => x.statusId == 3);
            this.isApprovedData = data.filter(x => x.statusId == 5);
            if(isDraftData.length > 0)
            {
              this.addDraftDataForSubmition(isDraftData);
              this.isFormDisable = false;
              this.isHeaderDisable = true;
            }
            if(isRejectedData.length > 0)
            {
              this.addRejectedDataForSubmition(isRejectedData);
            }
            if((data[0].statusId == 3) && (this.userDetails.RoleId == 2 || this.userDetails.RoleId == 3))
            {
              this.isFormDisable = true;
              this.isHeaderDisable = true;
            }
            if((data[0].statusId == 1 || data[0].statusId == 5) && (this.userDetails.RoleId == 3))
            {
              this.isApproveDisable = true;
              this.isRejectDisable = true;
            }
            if(data[0].statusId == 5){
              this.isApproveDisable = true;
              this.isRejectDisable = true;
            }
            if((data[0].statusId == 3) && (this.requestCreatedBy == this.userDetails.UserId))
            {
              this.isFormDisable = false;
            }
            if(this.userDetails.RoleId == 1)
            {
              if((data[0].statusId == 3) && (this.requestCreatedBy != this.userDetails.UserId))
              {
                this.isFormDisable = true;
              }
            }
          }
        }
        this.inputFormArray = data;
        this.actualInputFormArray = data;
        if(data.length == this.isApprovedData.length)
        {
          this.getPageTitle(5);
        }
        else{
          this.getPageTitle(this.paramStatusId);
        }
        
        for(let i=0; i< this.inputFormArray.length; i++)
        {
          if(this.inputFormArray[i].formDetailId)
          {
            this.inputFormArray[i].isSelected = true;
          }
        }
        for(let j=0;j< data.length;j++)
        {  
          this.totalAmount+= data[j].incentiveAmount;  
        }
      }

      this.isPrevWeekFormDisable = false;
      if(this.userDetails.RoleId != 3)
      {
        if(this.weekId != this.incentiveAppCalendar.weekId)
        {
          this.isPrevWeekFormDisable = true;
        }
      }

      
    });

    
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 50,
      lengthMenu : [5, 10, 25, 50, 75, 100],
      processing: true, 
      retrieve: true,
      order : [1, 'asc']
    };
  }

  getPageTitle(status)
  {
     if(!status && this.paramFormHeaderId) 
     {
       this.pageTitle = 'In Progress Input Form : ' + this.paramFormHeaderId
     }
     else if(status == 3)
     {
      this.pageTitle = 'Rejected Input Form : ' + this.paramFormHeaderId
     }
     else if(status == 4)
     {
      this.pageTitle = 'Draft Input Form : ' + this.paramFormHeaderId
     }
     else if(status == 5)
     {
      this.pageTitle = 'Approved Input Form : ' + this.paramFormHeaderId
     }
     else
     {
      this.pageTitle = 'New Input Form';
     }
  }

  addDraftDataForSubmition(draftData)
  {
    debugger;
    for(let i=0;i< draftData.length; i++)
    {
      if(draftData[i].createdBy == this.userDetails.UserId 
        && draftData[i].formHeaderId == this.paramFormHeaderId)
      {
        draftData[i].userId = this.userDetails.UserId;
        draftData[i].weekId = this.weekId;
        draftData[i].incentiveAmount = parseFloat(draftData[i].incentiveAmount);
        draftData[i].branchId = this.branchId;
        draftData[i].incentiveWeekDate = this.weekDate;
        draftData[i].formHeaderId = this.paramFormHeaderId;
        this.deleteInputFormArray.push(draftData[i]);
        this.inputFormDetails.push(draftData[i]);
      }
    }
  }
  addRejectedDataForSubmition(rejectedData)
  {
    for(let i=0;i< rejectedData.length; i++)
    {
      rejectedData[i].userId = this.userDetails.UserId;
      rejectedData[i].weekId = this.weekId;
      rejectedData[i].incentiveAmount = parseFloat(rejectedData[i].incentiveAmount);
      rejectedData[i].branchId = this.branchId;
      rejectedData[i].incentiveWeekDate = this.weekDate;
      rejectedData[i].formHeaderId = this.paramFormHeaderId;
      this.deleteInputFormArray.push(rejectedData[i]);
      this.inputFormDetails.push(rejectedData[i]);
    }
  }

  getAllUserBranches(userId){
    this.masterService.getUserBranches(this.userDetails.UserId).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.branchMasterArray = data;
        if(!this.urlBranchId)
        {
          this.selected = data[0].branchId;
          this.branchName = this.branchMasterArray[0].branchName;
        }
        else{
          this.branchName = this.branchMasterArray.find(x => x.branchId == this.branchId).branchName;
        }

        this.getAllPrimaryJob(this.branchId);
      }
    });
  }

  getAllBranches(option, regionCode){
    this.masterService.getAllBranches(option, regionCode).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.branchMasterArray = data;
        if(!this.urlBranchId)
        {
          this.selected = data[0].branchId;
          this.branchName = this.branchMasterArray[0].branchName;
        }
        else{
          this.branchName = this.branchMasterArray.find(x => x.branchId == this.branchId).branchName;
        }

        this.getAllPrimaryJob(this.branchId);
      }
    });
  }

  onBranchChange(branchOption)
  {
    //debugger
    this.branchId = branchOption.value;
    this.branchName = this.branchMasterArray.find(x => x.branchId == this.branchId).branchName;
    this.weekDate = new Date();
    this.weekNumber = this.weekId;
    this.primaryJobSelected = 'All';
    this.getAllPrimaryJob(this.branchId);
    this.getInpuFormDetails(this.branchId,this.weekNumber);
  }
  
  getInpuFormDetails(branchId, weekId){
    this.spinner.showSpinner();
    this.inputFormArray = [];
    this.actualInputFormArray = [];
    this.inputFormRequest.branchId = branchId;
    this.inputFormRequest.weekId = weekId;
    this.totalAmount= 0;
    this.inputFormService.getInputFormDetails(this.inputFormRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.spinner.stopSpinner();
        this.inputFormArray = data;
        //this.formHeaderCommnets = data[0].formHeaderComments;
        this.actualInputFormArray = data;
        for(let i=0; i< this.inputFormArray.length; i++)
        {
          if(this.inputFormArray[i].formDetailId)
          {
            this.inputFormArray[i].isSelected = true;
          }
        }
        
        for(let j=0;j< data.length;j++){  
          this.totalAmount+= data[j].incentiveAmount;  
        }
      }
    });
  }

  onSubmit() {
    debugger
    if(this.paramStatusId == 4)
    {
      var filterDraftData = this.inputFormDetails.filter(x => x.statusId == 4)
      if(filterDraftData.length > 0)
      {
        this.inputFormDetails = [];
        this.inputFormDetails = filterDraftData;
      }
    }
    if(this.inputFormDetails.length > 0){
      //check for any selection with toDelete is 1
      const toDeleteData = this.inputFormDetails.filter(x => x.toDelete == 1);
      if(toDeleteData.length == this.inputFormDetails.length)
      {
        Swal.fire('Please Select At Least One Employee Record', '', 'warning');
        return false;
      }

      const finalSubmitData = this.inputFormDetails.filter(x => x.toDelete != 1);
      let totalAmt = 0;
      for(let j=0;j< finalSubmitData.length;j++){  
        totalAmt+= finalSubmitData[j].incentiveAmount;  
      }
      
      Swal.fire({
        title: 'Total Incentive on this Form ' + this.cp.transform(totalAmt) ,
        text: "Okay to Submit?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
      }).then((result) => {
        if (result.isConfirmed) {
          this.spinner.showSpinner();
          this.inputFormDetails[0].attachment = this.fileNames;
          this.inputFormDetails[0].formHeaderComments = this.formHeaderCommnets;
          this.inputFormDetails.forEach(element => {
            element.statusId = 1;
            element.roleId = this.userDetails.RoleId;
          })
          this.inputFormService.insertInputForm(this.inputFormDetails).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.uploadFile(data[0].formHeaderId);
              this.spinner.stopSpinner();
              Swal.fire({
                icon : 'success',
                text: "Incentive details submitted successfuly",
                type: "success"
              }).then(() => {
                this.router.navigate(['/home']);
              })
            }
          });
        }
      })
    }
    else{
      Swal.fire('Please Select At Least One Employee Record', '', 'warning');
    }
  }

  onDraft() {
    //debugger
    if(this.paramStatusId == 4)
    {
      var filterDraftData = this.inputFormDetails.filter(x => x.statusId == 4)
      if(filterDraftData.length > 0)
      {
        this.inputFormDetails = [];
        this.inputFormDetails = filterDraftData;
      }
    }
    if(this.inputFormDetails.length > 0){
      const toDeleteData = this.inputFormDetails.filter(x => x.toDelete == 1);
      if(toDeleteData.length == this.inputFormDetails.length)
      {
        Swal.fire('Please Select At Least One Employee Record', '', 'warning');
        return false;
      }
      
      const finalSubmitData = this.inputFormDetails.filter(x => x.toDelete != 1);
      let totalAmt = 0;
      for(let j=0;j< finalSubmitData.length;j++){  
        totalAmt+= finalSubmitData[j].incentiveAmount;  
      }
      Swal.fire({
        title: 'Total Incentive on this Form ' + this.cp.transform(totalAmt) ,
        text: "Okay to Draft?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#103355',
        cancelButtonColor: '#7A1316',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
      }).then((result) => {
        if (result.isConfirmed) {
          this.spinner.showSpinner();
          this.inputFormDetails[0].attachment = this.fileNames;
          this.inputFormDetails[0].formHeaderComments = this.formHeaderCommnets;
          this.inputFormDetails.forEach(element => {
            element.statusId = 4;
          })
          this.inputFormService.insertInputForm(this.inputFormDetails).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.uploadFile(data[0].formHeaderId);
              this.spinner.stopSpinner();
              Swal.fire({
                icon : 'success',
                text: "Incentive details drafted successfuly",
                type: "success"
              }).then(() => {
                this.router.navigate(['/home']);
              })
            }
          });
        }
      })
    }
    else{
      Swal.fire('Please Select At Least One Employee Record', '', 'warning');
    }
  }

  selection(selectedObj, values, isChecked){
    debugger
    if(!selectedObj.incentiveAmount || selectedObj.incentiveAmount == "0" || parseFloat(selectedObj.incentiveAmount) == 0)
    {
      values.currentTarget.checked = false;
      //Swal.fire('Yikes!', 'Something went wrong!', 'error')
      Swal.fire('Please input incentive amount', '', 'warning');
    }
    else{
      if(parseFloat(selectedObj.incentiveAmount) < 0.01 || parseFloat(selectedObj.incentiveAmount) > 5000)
      {
        values.currentTarget.checked = false;
        Swal.fire('Please input amount in between $0.01 to $5000', '', 'warning');
        return false;
      }
      if(isChecked)
      {
        selectedObj.userId = this.userDetails.UserId;
        selectedObj.weekId = this.weekId;
        selectedObj.incentiveAmount = parseFloat(selectedObj.incentiveAmount);
        selectedObj.branchId = this.branchId;
        selectedObj.incentiveWeekDate = this.weekDate;
        selectedObj.toDelete = 0;
        if(this.paramStatusId == 4)
        {
          selectedObj.formHeaderId = this.paramFormHeaderId;
          selectedObj.statusId = 4;
        }

        if (!this.inputFormDetails.find((x) => x.employeeId == selectedObj.employeeId)) {
          this.inputFormDetails.push(selectedObj);
        }
        if (!this.deleteInputFormArray.find((x) => x.employeeId == selectedObj.employeeId)) {
          this.deleteInputFormArray.push(selectedObj);
        }
      }
      else{
        if(this.paramStatusId == 4 || this.paramStatusId == 3)
        {
          for( let i = 0; i < this.inputFormDetails.length; i++){ 
            if (this.inputFormDetails[i].employeeId == selectedObj.employeeId) { 
              this.inputFormDetails[i].toDelete = 1;
            }
          }
        }
        else{
          for( let i = 0; i < this.inputFormDetails.length; i++){ 
            if (this.inputFormDetails[i].employeeId == selectedObj.employeeId) { 
              this.inputFormDetails.splice(i, 1); 
            }
          }
        }
        
        for( let i = 0; i < this.deleteInputFormArray.length; i++){ 
          if (this.deleteInputFormArray[i].employeeId == selectedObj.employeeId) { 
            this.deleteInputFormArray.splice(i, 1); 
          }
        }
      }
    }
  }

  onAmountChange(obj, $event, index)
  {
    debugger
    if(this.inputFormArray[index].isSelected == true)
    {
      if($event > 5000 || $event <= 0)
      {
        this.inputFormArray[index].isSelected = false;
        if(this.paramStatusId == 4)
        {
          for( let i = 0; i < this.inputFormDetails.length; i++){ 
            if (this.inputFormDetails[i].employeeId == obj.employeeId) { 
              this.inputFormDetails[i].toDelete = 1;
            }
          }
        }
        else
        {
          for( let i = 0; i < this.inputFormDetails.length; i++){ 
            if (this.inputFormDetails[i].employeeId === obj.employeeId) { 
              this.inputFormDetails.splice(i, 1); 
            }
          }
        }
        
        Swal.fire('Please input amount in between $0.01 to $5000', '', 'warning');
      }
      for( let i = 0; i < this.inputFormDetails.length; i++){ 
        if (this.inputFormDetails[i].employeeId === obj.employeeId) { 
          this.inputFormDetails[i].incentiveAmount = $event;
        }
      }
    }
  }

  onCommentsChange(obj, $event, index)
  {
    for( let i = 0; i < this.inputFormDetails.length; i++){ 
      if (this.inputFormDetails[i].employeeId === obj.employeeId) { 
        this.inputFormDetails[i].comments = $event;
      }
    }
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //debugger
    //this.events.push(`${type}: ${event.value}`);
    this.weekNumber = this.getWeekNumber(event.value);
    this.getInpuFormDetails(this.branchId, this.weekNumber);
  }

  getAuditTrailDetails(formHeaderId, branchId){
    //debugger
    //request : DashboardRequest;
    this.dashboradRequest.FormHeaderId = formHeaderId;
    this.dashboradRequest.BranchId = branchId;
    this.auditTrailDetails = [];
    this.dashboardService.getAuditTrailDetails(this.dashboradRequest).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.auditTrailDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

  public getWeekNumber(d : Date) : string{
    //debugger
    return d.getFullYear() % 100 + '_W' +  moment(d).week();
    //return moment(d).week() - moment(d).startOf('month').week() + 1 + '' + moment(d).week() + '' + d.getFullYear();
  }

  onDeleteClick(){
    // if(this.inputFormArray.length != this.deleteInputFormArray.length)
    // {
    //   Swal.fire('Please select all checkboxes to delete this form', '', 'warning');
    //   return false;
    // }
    debugger;
    if(this.paramStatusId == 4)
    {
      var filterDraftData = this.deleteInputFormArray.filter(x => x.statusId == 4)
      if(filterDraftData.length > 0)
      {
        this.deleteInputFormArray = [];
        this.deleteInputFormArray = filterDraftData;
      }
    }
    if(this.deleteInputFormArray.length > 0)
    {
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
          if(this.inputFormArray.length == this.deleteInputFormArray.length)
          {
            this.deleteInputFormArray.forEach(element => {
              element.option = 0;
            })
          }
          else{
            this.deleteInputFormArray.forEach(element => {
              element.option = 1;
            })
          }
          this.dashboardService.deleteInputForm(this.deleteInputFormArray).subscribe(data => {
            //debugger
            if(data){
              //debugger
              this.spinner.stopSpinner(); 
              this.inputFormDetails = [];
              this.deleteInputFormArray = [];
              this.getInpuFormDetails(this.branchId,this.weekNumber);
              Swal.fire(
                'Deleted!',
                'Your have deleted the incentive form.',
                'success'
              ).then(() =>{
                this.router.navigate(['/home']);
              })
              // Swal.fire(
              //   'Deleted!',
              //   'Your have deleted the incentive form.',
              //   'success'
              // )
            }
          });
        }
      })
    }
    else
    {
      Swal.fire('Please Select At Least One', '', 'warning');
    }
  }

  onRejectForm()
  {
    debugger;
    this.actionObj.formHeaderId = this.paramFormHeaderId;
    this.actionObj.userId = this.userDetails.UserId;
    this.actionObj.roleId = this.userDetails.RoleId;
    this.actionObj.branchId = this.userDetails.BranchId;
    this.actionObj.createdBy = this.createdByForRejectEmail;
    this.actionArray = [];
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
        this.actionObj.statusId = 3;
        this.actionObj.comments = result.value;
        this.actionArray.push(this.actionObj);
        this.spinner.showSpinner(); 
        this.dashboardService.reviewerActionClick(this.actionArray).subscribe(data => {
          //debugger
          if(data){
            //debugger
            this.spinner.stopSpinner(); 
            Swal.fire(
              'Rejected!',
              'Your have rejected the incentive form.',
              'success'
            ).then(() =>{
              if(this.userDetails.RoleId == 3){
                this.router.navigate(['/payroll-review']);
              }
              else{
                this.router.navigate(['/home']);
              }
            })
          }
        });
      }
    })
  }

  onApproveForm()
  {
    this.actionObj.formHeaderId = this.paramFormHeaderId;
    this.actionObj.userId = this.userDetails.UserId;
    this.actionObj.roleId = this.userDetails.RoleId;
    this.actionObj.branchId = this.userDetails.BranchId;
    this.actionObj.weekId = this.weekNumber;
    this.actionArray = [];
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
            Swal.fire(
              'Approved!',
              'Your have approved the incentive form.',
              'success'
            ).then(() =>{
              if(this.userDetails.RoleId == 3){
                this.router.navigate(['/payroll-review']);
              }
              else{
                this.router.navigate(['/home']);
              }
            })
          }
        });
      }
    })
  }

  allTabsValid(): boolean {
    //debugger
    if (this.inputForm.invalid) {
      //this.formTabs.tabs[0].active = true;
      return false;
    }
    return true;
  }


  rangeRestriction(event, value, obj) 
  {
    //debugger
    const min = 0;
    const max = 5000;
    if(value == '0.00')
    {
      obj.incentiveAmount = null;
    }
    if(parseInt(value) < min || isNaN(parseInt(value))) 
    {
      obj.incentiveAmount = null;
    }
    else if(parseInt(value) > max || isNaN(parseInt(value)))
    {
      obj.incentiveAmount = null;
    }
  }

  uploadFile(formHeaderId): void {
    for (let index = 0; index < this.theFiles.length; index++) {
        this.readAndUploadFile(this.theFiles[index], formHeaderId );
    }
  }

  onFileChange(event) {
    debugger;
    this.theFiles = [];
    this.fileUploadModelList = [];
    this.fileNames = null;
    const fileNameList = [];
    // Any file(s) selected from the input?
    if (event.target.files && event.target.files.length > 0) {
        for (let index = 0; index < event.target.files.length; index++) {
            let file = event.target.files[index];
            let fileModel = new FileToUpload();
            // Don't allow file sizes over 1MB
            if (file.size < MAX_SIZE) {
                // Add file to list of Attachment
                this.theFiles.push(file);
                if(this.paramFormHeaderId)
                {
                  fileNameList.push(this.paramFormHeaderId + '_' + file.name);
                }
                else
                {
                  fileNameList.push(file.name);
                }
            }
            else {
              Swal.fire("File: " + file.name + " is too large to upload.", '', 'warning');
            }
        }
    }
    var finalFileNameList = [];
    if(this.paramFormHeaderId)
    {
      if(this.files.length > 0)
      {
        var newFileList = [];
        for(var i=0; i< fileNameList.length; i++){
         // var paramFileName = this.paramFormHeaderId + '_' + fileNameList[i];
          var fileMatchRes = this.files.filter(x => x.fileName == fileNameList[i]);
          if(fileMatchRes.length == 0)
          {
            newFileList.push(fileNameList[i]);
          }
        }
        for(var i=0; i< this.files.length; i++)
        {
          finalFileNameList.push(this.files[i].fileName);
        }
        for(var i=0; i< newFileList.length; i++)
        {
          finalFileNameList.push(newFileList[i]);
        }
        this.fileNames = finalFileNameList.join();
      }
      else{
        this.fileNames = fileNameList.join();
      }
    }
    else{
      this.fileNames = fileNameList.join();
    }
  }

  private readAndUploadFile(theFile: any, updatedfileName : string) {
    debugger;
    let file = new FileToUpload();
    
    debugger;
    // Set File Information
    file.fileName = theFile.name;
    file.fileSize = theFile.size;
    file.fileType = theFile.type;
    file.lastModifiedTime = theFile.lastModified;
    file.lastModifiedDate = theFile.lastModifiedDate;
    file.updatedFileName = updatedfileName;
    // Use FileReader() object to get file to upload
    // NOTE: FileReader only works with newer browsers
    let reader = new FileReader();
    // Setup onload event for reader
    reader.onload = () => {
        //debugger
        // Store base64 encoded representation of file
        file.fileAsBase64 = reader.result.toString();
        // POST to server
        this.inputFormService.uploadFile(file).subscribe(resp => { 
            //debugger
            this.messages.push("Upload complete"); 
        });
    }
    // Read the file
    reader.readAsDataURL(theFile);
  }

  getAllWeekIdDetails(){
    this.masterService.getAllWeekIdDetails().subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.weekIdArray = data;
        if(data.filter(x => x.weekId == this.weekId).length > 0)
        {
          this.payrollDate = data.filter(x => x.weekId == this.weekId)[0].payrollDate;
        }
      }
    });
  }

  onItemSelect(item: any) {
    this.filterDataOnPrimaryJob();
  }
  onSelectAll(items: any) {
    this.filterDataOnPrimaryJob();
  }
  onItemDeSelect($event){
    this.filterDataOnPrimaryJob();
  }
  onDeSelectAll($event){
    this.filterDataOnPrimaryJob();
  }

  getAllPrimaryJob(branchId){
    this.masterService.getPrimaryJobs(branchId).subscribe(data => {
      //debugger
      if(data){
        //debugger
        this.primaryJobsArray = data;
        this.selectedItems = data;
        this.dropdownSettings = {
          singleSelection: false,
          idField: 'primaryJob',
          textField: 'primaryJob',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 1
        };
      }
    });

  }

  filterDataOnPrimaryJob(){
    debugger;
    //this.primaryJobSelected = primaryJob.value;
    this.inputFormArray = [];
    this.spinner.showSpinner();
    this.totalAmount = 0;
    setTimeout(()=>{    
      debugger;   
      // Use map to get a simple array of "primaryJobs" values. Ex: ['Driver', 'Admin']
      let yFilter = this.selectedItems.map(itemY => { return itemY.primaryJob; });

      // Use filter and includes to filter the full dataset by the filter dataset's val.
      let filteredX = this.actualInputFormArray.filter(itemX => yFilter.includes(itemX.primaryJob));

      this.inputFormArray = filteredX;
      for(let j=0;j< this.inputFormArray.length;j++){  
        this.totalAmount+= this.inputFormArray[j].incentiveAmount;  
      }        
      this.spinner.stopSpinner();
    }, 500);
    
  }

  onPrimaryJobChange(primaryJob){
    debugger;
    this.primaryJobSelected = primaryJob.value;
    this.inputFormArray = [];
    this.spinner.showSpinner();
    this.totalAmount = 0;
    setTimeout(()=>{    
      debugger;   
      if(this.primaryJobSelected == 'All')
      {
        this.inputFormArray = this.actualInputFormArray;
      }
      else{
        this.inputFormArray = this.actualInputFormArray.filter(p => p.primaryJob == this.primaryJobSelected)
      }
      for(let j=0;j< this.inputFormArray.length;j++){  
        this.totalAmount+= this.inputFormArray[j].incentiveAmount;  
      }
      this.spinner.stopSpinner();
    }, 1000);
    
  }

  private getFiles(formHeaderId) {
    this.uploadDownloadService.getFiles(formHeaderId).subscribe(
      data => {
        debugger;
        this.files = data.filter(x => x.fileName != null);
        var commentsObj = data.filter(x => x.formHeaderComments != null);
        if(commentsObj.length > 0)
        {
          this.formHeaderCommnets = commentsObj[0].formHeaderComments;
        }
      }
    );
  }

  public downloadStatus(event: ProgressStatus) {
    switch (event.status) {
      case ProgressStatusEnum.START:
        this.showDownloadError = false;
        break;
      case ProgressStatusEnum.IN_PROGRESS:
        this.showProgress = true;
        this.percentage = event.percentage;
        break;
      case ProgressStatusEnum.COMPLETE:
        this.showProgress = false;
        break;
      case ProgressStatusEnum.ERROR:
        this.showProgress = false;
        this.showDownloadError = true;
        break;
    }
  }
  
}



export class FileUploadModel {
  data: File;
  state: string;
  inProgress: boolean;
  progress: number;
  canRetry: boolean;
  canCancel: boolean;
}
export interface InputForm {
  fBMId: string;
  employeeId: number;
  employeeName: string;
  location: string;
  payGroup: string;
  incentiveAmount: number;
  formHeaderId: string;
  formDetailId : string;
  weekId: string;
  checked : false;
  createdDate : Date;
  incentiveWeekDate : Date;
  statusId : number;
  branchId : string;
  comments : string;
  filename : string;
  attachment : string;
}