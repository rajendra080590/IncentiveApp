import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmployeeDetails } from 'app/model/EmployeeDetails';
import { FileToUpload } from 'app/model/FileToUpload';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { Inputform } from 'app/model/inputform';
import { InputForm } from 'app/table-list/table-list.component';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InputformService {
  mainURL : string = environment.mainUrl;
  //mainURL : string = "https://fbmdevwebapp01.azurewebsites.net/";
  branchId : string;
  weekId : string;
  constructor(private http:HttpClient) { }
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  // getInpuFormDetails1(branchId, weekId, formHeaderId) : Observable<InputForm[]> {
  //   const baseURL: string = "http://localhost:50869/api/NewForm/GetNewFormDetails/" + branchId + "/" + weekId + "/" + formHeaderId;

  //   return this.http.get<InputForm[]>(baseURL);
  // }

  getInputFormDetails(InputForm) : Observable<EmployeeDetails[]> {
    const baseURL: string = this.mainURL + "api/NewForm/GetNewFormDetails";
    
    const body=InputForm;
    
    return this.http.post<EmployeeDetails[]>(baseURL, body, {
      headers: this.headers
    })
  }

  insertInputForm(Inputform) : Observable<Inputform>{
    const baseURL: string = this.mainURL + "api/NewForm/Upsert";
    const body=Inputform;
    
    return this.http.post<Inputform>(baseURL, body, {
      headers: this.headers
    })
  }

  

  insertDraftInputForm(Inputform) : Observable<Inputform>{
    const baseURL: string = this.mainURL + "api/NewForm/DraftUpsert";
    const body=Inputform;
    
    return this.http.post<Inputform>(baseURL, body, {
      headers: this.headers
    })
  }

  deleteInputForm(Inputform) : Observable<InputForm>{
    const baseURL: string = this.mainURL + "api/dashboard/DeleteInputForm";
    const body=Inputform;
    
    return this.http.post<InputForm>(baseURL, body, {
        headers: this.headers
    })
  }

  
  uploadFile(theFile: FileToUpload) : Observable<any> {
    debugger;
    const httpOptions = {
      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      })
    };
    const baseURL: string = this.mainURL + "api/FileUpload/";
    return this.http.post<FileToUpload>(baseURL, theFile, httpOptions);
  }

}
