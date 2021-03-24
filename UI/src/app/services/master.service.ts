import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { BranchMaster } from 'app/model/BranchMaster';
import { PayGroup } from 'app/model/PayGroup';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { States } from 'app/model/States';
import { Regions } from 'app/model/Regions';
import { environment } from 'environments/environment';
import { PrimaryJobs } from 'app/model/PrimaryJobs';

@Injectable({
  providedIn: 'root'
})
export class MasterService {
  //mainURL : string = "https://fbmdevwebapp01.azurewebsites.net/";
  mainURL : string = environment.mainUrl;
  userId : number;
  constructor(private http:HttpClient) { }

  getUserBranches(userId) : Observable<BranchMaster[]> {
    return this.http.get<BranchMaster[]>(this.mainURL + 'api/branch/GetBranchDetails/' + userId);
  }

  getPrimaryJobs(branchId) : Observable<PrimaryJobs[]> {
    return this.http.get<PrimaryJobs[]>(this.mainURL + 'api/branch/GetAllPrimaryJobs/' + branchId);
  }

  getAllBranches(option, stateCode) : Observable<BranchMaster[]> {
    return this.http.get<BranchMaster[]>(this.mainURL + 'api/branch/GetAllBranches/'+ option + '/' + stateCode );
  }

  getAllPayGroups() : Observable<PayGroup[]> {
    return this.http.get<PayGroup[]>(this.mainURL + 'api/branch/GetAllPayGroups');
  }

  getPayrollDateDetails(paayrollDateRequest) : Observable<IncentiveAppCalendar>{
    const baseURL: string = this.mainURL + "api/NewForm/GetPayrollDateDetails";
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body=paayrollDateRequest;
    
    return this.http.post<IncentiveAppCalendar>(baseURL, body, {
        headers: headers
    })
  }

  getAllStates(option, regionCode) : Observable<States[]> {
    return this.http.get<States[]>(this.mainURL + 'api/branch/getAllStates/' + option + '/' + regionCode);
  }

  getAllRegions(option, stateCode) : Observable<Regions[]> {
    return this.http.get<Regions[]>(this.mainURL + 'api/branch/getAllRegions/' + option + '/' + stateCode);
  }

  getAllWeekIdDetails() : Observable<IncentiveAppCalendar[]> {
    return this.http.get<IncentiveAppCalendar[]>(this.mainURL + 'api/payroll/GetWeekIdDetails');
  }

}
