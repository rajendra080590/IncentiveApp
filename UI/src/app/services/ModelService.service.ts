import { Injectable } from '@angular/core';
import { IncentiveAppCalendar } from 'app/model/IncentiveAppCalendar';
import { Userdata } from '../model/userdata';

@Injectable({
  providedIn: 'root'
})
export class ModelServiceService {
    private userDetails;
    private incentiveAppCalendar;
    constructor() { 
      this.userDetails = Userdata;
      this.incentiveAppCalendar = new IncentiveAppCalendar;
    }

    addIncentiveAppCalendar(item)
    {
      debugger;
      this.incentiveAppCalendar.payrollDate = item.payrollDate;
      this.incentiveAppCalendar.weekNumber = item.weekNumber;
      this.incentiveAppCalendar.weekId = item.weekId;
    }
    getIncentiveAppCalendar(): IncentiveAppCalendar {
      return this.incentiveAppCalendar;
    }
 
    addItem(item) {
        this.userDetails.UserId = item.userId;
        this.userDetails.RoleId = item.roleId;
        this.userDetails.BranchId = item.branchId;
        this.userDetails.FirstName = item.firstName;
        this.userDetails.LastName = item.lastName;
        this.userDetails.EmailId = item.emailId;
        this.userDetails.IsProd = false;
    }
 
    getItems(): Userdata {
        return this.userDetails;
    }

}
