import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuditTrail } from 'app/model/AuditTrail';
import { DashboardDetails } from 'app/model/DashboardDetails';
import { PayrollPending } from 'app/model/PayrollPending';
import { PayrollReview } from 'app/model/PayrollReview';
import { PayrollReviewed } from 'app/model/PayrollReviewed';
import { ReviewerAction } from 'app/model/ReviewerAction';
import { InputForm } from 'app/table-list/table-list.component';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {

  mainURL : string = environment.mainUrl;
    //mainURL : string = "https://fbmdevwebapp01.azurewebsites.net/";
    constructor(private http:HttpClient) { }

    getPayrollDownloadDetails(payrollPending) : Observable<PayrollPending[]>{
      const baseURL: string = this.mainURL + "api/payroll/GetPayrollDownloadDetails";
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      const body=payrollPending;
      
      return this.http.post<PayrollPending[]>(baseURL, body, {
          headers: headers
      })
    }

    getPayrollReviewDetails(payrollReview) : Observable<PayrollReview[]>{
      const baseURL: string = this.mainURL + "api/payroll/GetPayrollReviewDetails";
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      const body=payrollReview;
      
      return this.http.post<PayrollReview[]>(baseURL, body, {
          headers: headers
      })
    }

    payrollReviewedAction(payrollReviewed) : Observable<PayrollReviewed>{
      const baseURL: string =  this.mainURL + "api/payroll/UpsertPayrollReviewed";
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      const body=payrollReviewed;
      
      return this.http.post<PayrollReviewed>(baseURL, body, {
          headers: headers
      })
  }

}
