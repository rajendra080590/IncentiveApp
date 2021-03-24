import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuditTrail } from 'app/model/AuditTrail';
import { DashboardDetails } from 'app/model/DashboardDetails';
import { PayrollPending } from 'app/model/PayrollPending';
import { ReviewerAction } from 'app/model/ReviewerAction';
import { InputForm } from 'app/table-list/table-list.component';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class DashboardService {
    mainURL : string = environment.mainUrl;
    //mainURL : string = "https://fbmdevwebapp01.azurewebsites.net/";
    constructor(private http:HttpClient) { }

    getDashboardDetails(DashboardRequest) : Observable<DashboardDetails[]>{
        const baseURL: string = this.mainURL + "api/dashboard/GetDashboardDetails";
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const body=DashboardRequest;
        
        return this.http.post<DashboardDetails[]>(baseURL, body, {
            headers: headers
        })
    }

    getAuditTrailDetails(DashboardRequest) : Observable<AuditTrail[]>{
        const baseURL: string =  this.mainURL + "api/dashboard/GetAuditTrailDetails";
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const body=DashboardRequest;
        
        return this.http.post<AuditTrail[]>(baseURL, body, {
            headers: headers
        })
    }

    reviewerActionClick(reviewerAction) : Observable<ReviewerAction>{
        const baseURL: string =  this.mainURL + "api/dashboard/ReviewerAction";
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const body=reviewerAction;
        
        return this.http.post<ReviewerAction>(baseURL, body, {
            headers: headers
        })
    }

    deleteInputForm(Inputform) : Observable<InputForm>{
        const baseURL: string =  this.mainURL + "api/dashboard/DeleteInputForm";
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const body=Inputform;
        
        return this.http.post<InputForm>(baseURL, body, {
            headers: headers
        })
    }
    getPayrollPendingDetails(payrollPending) : Observable<PayrollPending[]>{
        const baseURL: string = this.mainURL + "api/dashboard/GetPayrollPendingDetails";
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const body=payrollPending;
        
        return this.http.post<PayrollPending[]>(baseURL, body, {
            headers: headers
        })
    }

}
