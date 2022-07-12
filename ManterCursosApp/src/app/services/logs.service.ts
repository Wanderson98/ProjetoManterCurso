import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Logs } from '../model/Logs';

const headersOptions ={
  headers: new HttpHeaders({
    'content-type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class LogsService {


  private urlApi = 'https://localhost:7181/api/Logs';

  constructor(private http: HttpClient) { }

  pegarLogs(): Observable<Logs[]>
  {
    return this.http.get<Logs[]>(this.urlApi, headersOptions)
  }
}
