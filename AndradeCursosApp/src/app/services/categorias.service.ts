import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categoria } from '../model/Categoria';

const headersOptions ={
  headers: new HttpHeaders({
    'content-type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {

  private urlApi = 'https://localhost:7181/api/Categorias';

  constructor(private http: HttpClient) { }

  ListarTodasCategorias(): Observable<Categoria[]>
  {
    return this.http.get<Categoria[]>(this.urlApi, headersOptions)
  }
}
