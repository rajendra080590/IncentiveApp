import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { FBMUsers} from '../model/FBMUsers';
import { environment } from 'environments/environment';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //mainURL : string = "https://fbmdevwebapp01.azurewebsites.net/";
  mainURL : string = environment.mainUrl;
  baseURL: string =  this.mainURL +"api/login/ValidateUser";
  constructor(private http:HttpClient, private msalService: MsalService) { }

  validateUser(user : FBMUsers) : Observable<FBMUsers[]> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    //const body=JSON.stringify(fbmUsers);
    user = {
      userName : user.userName, password : user.password, emailId : null, branchId : null, 
      firstName : null, userId : 0, roleId: null, lastName: null, isAdmin : 0
    }
    return this.http.post<FBMUsers[]>(this.baseURL, user, {
      headers: headers
    })
  }

  validateUserId(userId) : Observable<FBMUsers[]> {
    this.baseURL = this.mainURL + "api/login/ValidateUserId/" + userId;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    //const body=JSON.stringify(fbmUsers);
    return this.http.post<FBMUsers[]>(this.baseURL, userId, {
      headers: headers
    })
  }

  validateADUser(userEmail) : Observable<FBMUsers[]> {
    debugger;
    this.baseURL = this.mainURL + "api/login/ValidateADUser/" + userEmail;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 
    'Authorization': 'Bearer ' + localStorage.getItem('msal.idtoken') 
    //'Authorization': 'Bearer ' + localStorage.getItem('msal.token') 
  });
    //const body=JSON.stringify(fbmUsers);
    return this.http.post<FBMUsers[]>(this.baseURL, userEmail, {
      headers: headers
    })
  }


  httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
  };

  validateADUserTest(userEmail): Observable<FBMUsers> {
    debugger;
    this.baseURL = this.mainURL + "api/login/ValidateADUser/" + userEmail;
    this.httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('msal.idtoken') 
        })
    };

    return this.http.get(this.baseURL, this.httpOptions)
        .pipe((response: any) => {
            return response;
        });
  }



}
