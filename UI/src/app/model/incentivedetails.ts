import { Iincentivedetails} from './iincentivedetails';
export class Incentivedetails implements Iincentivedetails {
    FBMId : number;
    BranchNumber : number;
    BranchName : string;
    Week : number;
    SubmittedBy : string;
    TotalEmployee: number;
    Incetivized : number;
    TotalAmount : number;
    IsSelected : false;
    ReviewId : number;
}
