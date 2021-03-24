import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { FileDetails } from 'app/model/FileDetails';

@Injectable({
  providedIn: 'root'
})
export class UploadDownloadService {

  private baseApiUrl: string;
  private mainURL : string = environment.mainUrl;
  private apiDownloadUrl: string;
  private apiUploadUrl: string;
  private apiFileUrl: string;


  constructor(private httpClient: HttpClient) {
    //this.mainURL = "https://fbmdevwebapp01.azurewebsites.net/";
    //this.mainURL = "http://localhost:50869/";
    this.baseApiUrl = 'https://localhost:44398/api/FileDownload/';
    this.apiDownloadUrl = this.mainURL + 'api/FileUpload/download';
    this.apiUploadUrl = this.mainURL + 'upload';
    this.apiFileUrl = this.mainURL + 'api/FileUpload/files';
  }

  public downloadFile(file: string): Observable<HttpEvent<Blob>> {
    return this.httpClient.request(new HttpRequest(
      'GET',
      `${this.apiDownloadUrl}?file=${file}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }

  public uploadFile(file: Blob): Observable<HttpEvent<void>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.httpClient.request(new HttpRequest(
      'POST',
      this.apiUploadUrl,
      formData,
      {
        reportProgress: true
      }));
  }

  public getFiles(formHeaderId): Observable<FileDetails[]> {
    debugger;
    return this.httpClient.get<FileDetails[]>(this.apiFileUrl + '/' + formHeaderId);
  }

}
