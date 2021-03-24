import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuditTrail } from 'app/model/AuditTrail';
import { DashboardRequest } from 'app/model/DashboardRequest';
import { DashboardService } from 'app/services/dashboard.service';
import { SpinnerService } from 'app/services/spinner.service';
import { DialogData } from '../dashboard.component';

@Component({
  selector: 'app-audittraildialog',
  templateUrl: './audittraildialog.component.html',
  styleUrls: ['./audittraildialog.css']
})
export class AudittraildialogComponent implements OnInit {
  auditDtOptions: DataTables.Settings = {};
  auditTrailDetails : AuditTrail [] = [];
  dashboradRequest : DashboardRequest;
  constructor(public spinner : SpinnerService,private dashboardService : DashboardService,
    public dialogRef: MatDialogRef<AudittraildialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
      this.dashboradRequest = new DashboardRequest;
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    debugger;
    //console.log(this.data);
    this.spinner.showSpinner();
    this.getAuditTrailDetails(this.data);
    this.auditDtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [3, 5, 10],
      processing: true,
      ordering : false,
    };
  }

  getAuditTrailDetails(auditOption){
    debugger;
    //request : DashboardRequest;
    this.dashboradRequest.FormHeaderId = auditOption.formHeaderId;
    this.dashboradRequest.BranchId = auditOption.branchId;
    this.auditTrailDetails = [];
    this.dashboardService.getAuditTrailDetails(this.dashboradRequest).subscribe(data => {
      debugger;
      if(data){
        debugger;
        this.auditTrailDetails = data;
        this.spinner.stopSpinner(); 
      }
    });
  }

}
