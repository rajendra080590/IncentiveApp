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
import { Regions } from 'app/model/Regions';
import { States } from 'app/model/States';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { PayrollService } from 'app/services/payroll.service';

const file_Extention = '.csv';

@Component({
  selector: 'app-payroll-pending',
  templateUrl: './payroll-pending.component.html',
  styleUrls: ['./payroll-pending.component.css']
})
export class PayrollPendingComponent implements OnInit {

  payrollPendingDetails : PayrollPending[] = [];
  payrollPendingRequest : PayrollPending;
  //dtOptions: DataTables.Settings = {};
  dtOptions: any = {};
  public userDetails : Userdata;
  public branchMasterArray : BranchMaster[];
  public payGroupArray : PayGroup[];
  public regionsArray : Regions[];
  public statesArray : States[];
  public weekIdArray : IncentiveAppCalendar[];
  branchList = new FormControl();
  payGroupName = new FormControl();
  weekDate : Date; weekNumber : string = null;weekId : string;  payrollDate : Date;
  totalBranches : number; totalAmount : number = 0; totalSubmittedForm : number = 0;
  totalBranchApproved : number = 0; totalPayrollApproved : number = 0;
  regionCode : string = 'All';stateCode : string = 'All'; branchId : string = 'All'; payGroup : string = 'All';
  @Input()
  max: Date | null;
  today = new Date(); 
  incentiveAppCalendar : IncentiveAppCalendar;
  constructor(public spinner : SpinnerService, private masterService : MasterService, private router: Router,
    private modelService : ModelServiceService, private dashboardService : DashboardService,private payrollService : PayrollService,
    private titleCasePipe : TitleCasePipe) { 
      this.userDetails = this.modelService.getItems();
      this.incentiveAppCalendar = this.modelService.getIncentiveAppCalendar();
      //this.weekNumber = this.getWeekNumber(this.weekDate);
      this.weekId = this.incentiveAppCalendar.weekId;
      this.payrollDate = this.incentiveAppCalendar.payrollDate;
      this.payrollPendingRequest = new PayrollPending;
      this.today.setDate(this.today.getDate());
      this.dtOptions = {
        pagingType: 'full_numbers',
        pageLength: 25,
        lengthMenu : [5, 10, 25,50],
        processing: true,
        ordering : true,
        order : [[9, 'asc'],[6, 'asc']],
        dom: 'Bfrtip',
        buttons: [
          { 
            extend: 'csv', text : "EXPORT TO CSV", className: 'btn-csv' ,
            title : 'PayrollDownload_' + new Date().getTime() 
          }
        ],
        columnDefs: [
          { "orderable": true, "targets": 1 }
        ]
      };
    }

  ngOnInit() {
    this.spinner.showSpinner(); 
    
    this.getAllBranches(0,0);
    this.getAllPayGroups();
    this.getAllWeekIdDetails();
    this.getAllRegions(0, null);
    this.getAllStates(0,0);
    this.getPayrollPendingDetails();
  }

  getPayrollPendingDetails(){
    //debugger;
    this.payrollPendingRequest.statusId = 5;
    this.payrollPendingRequest.option = 1;
    this.payrollPendingRequest.weekId = this.weekId;
    this.payrollPendingDetails = [];
    this.payrollService.getPayrollDownloadDetails(this.payrollPendingRequest).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.payrollPendingDetails = data;
        this.getSubmittedDetails(data);
        this.spinner.stopSpinner(); 
      }
    });
  }

  getSubmittedDetails(data)
  {
    this.totalAmount = 0;
    this.totalSubmittedForm = data.length;
    this.totalPayrollApproved = data.length;
    for(let j=0;j< data.length;j++){  
      this.totalAmount+= data[j].incentiveAmount;  
    }  
  }

  searchOnClick(){
    //debugger;
    var resBranches;
    if(this.branchList.value)
    {
      resBranches = this.branchList.value.join();
    }
    this.payrollPendingRequest.statusId = 5;
    this.payrollPendingRequest.option = 2;
    this.payrollPendingRequest.weekId = this.weekId;
    this.payrollPendingRequest.payGroupCode = this.payGroup == 'All' ? null : this.payGroup;
    this.payrollPendingRequest.regionCode = this.regionCode == 'All' ? null : parseInt(this.regionCode);
    this.payrollPendingRequest.stateCode = this.stateCode == 'All' ? null : this.stateCode;
    this.payrollPendingRequest.branchId = this.branchId == 'All' ? null : this.branchId;

    this.payrollPendingDetails = [];
    this.spinner.showSpinner(); 
    this.payrollService.getPayrollDownloadDetails(this.payrollPendingRequest).subscribe(data => {
      //debugger;
      if(data){
        //debugger;
        this.payrollPendingDetails = data;
        this.getSubmittedDetails(data);
        this.spinner.stopSpinner(); 
      }
    });
    //console.log(this.branchList.value + '' + this.weekNumber + '' + this.payGroupName.value)
  }

  clearClick()
  {
    this.spinner.showSpinner(); 
    this.branchList.setValue("");
    this.weekNumber = null;
    this.weekDate = null;
    this.payGroupName.setValue("");
    this.getPayrollPendingDetails();
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


  weekNumberChange($event)
  {
    //debugger;
    this.payrollDate = this.weekIdArray.filter(x => x.weekId == $event.value)[0].payrollDate;
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
  
  downloadCSV(){
    //debugger;
      const replacer = (key, value) => value === null ? '' : value; // specify how you want to handle null values here
      const headerObj =  new PayrollDownload;
      headerObj.companyCode = this.payrollPendingDetails[0].companyCode;
      headerObj.employeeId = this.payrollPendingDetails[0].employeeId;
      headerObj.earnCode = this.payrollPendingDetails[0].earnCode;
      headerObj.emptyColumn = this.payrollPendingDetails[0].emptyColumn;
      headerObj.blanckColumn = this.payrollPendingDetails[0].blanckColumn;
      headerObj.earnAmt = this.payrollPendingDetails[0].earnAmt;
      headerObj.employeeName = this.payrollPendingDetails[0].employeeName;
      headerObj.payGroupCode = this.payrollPendingDetails[0].payGroupCode;
      headerObj.locationCode = this.payrollPendingDetails[0].locationCode;
      const header = Object.keys(headerObj);
      let csv = this.payrollPendingDetails.map(row => header.map(fieldName => JSON.stringify(row[fieldName], replacer)).join(','));

      const titleCase = new TitleCasePipe();
      const titleCaseObj = titleCase.transform(header.join(','));
      csv.unshift(titleCaseObj.toLocaleUpperCase());
      let csvArray = csv.join('\r\n');
  
      var blob = new Blob([csvArray], {type: 'text/csv' })
      const fileName = 'PayrollDownload_' + new Date().getTime() + file_Extention
      fileSaver.saveAs(blob, fileName);
  }

}

export class WeekModel {
  weekId : string;
}
