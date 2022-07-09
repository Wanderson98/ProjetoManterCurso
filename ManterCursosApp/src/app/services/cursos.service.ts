import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Curso } from '../model/Curso';

const headersOptions ={
  headers: new HttpHeaders({
    'content-type': 'application/json'
  })
}
@Injectable({
  providedIn: 'root'
})
export class CursosService {
 
  private urlApi = 'https://localhost:7181/api/Cursos';

  constructor(private http: HttpClient) { }

  AdicionarCurso(curso:Curso) : Observable<Curso>
  {
      return this.http.post<Curso>(this.urlApi, curso, headersOptions)
  }

  ListarTodosCursos() : Observable<Curso[]>
  {
    return this.http.get<Curso[]>(this.urlApi, headersOptions)
  }

  PegarCursoPorId(cursoId:number) : Observable<Curso>
  {
    const url = `${this.urlApi}/${cursoId}`;
    return this.http.get<Curso>(url, headersOptions)
  }

  ListarTodosCursosAtivos() :  Observable<Curso[]>
  {
    const url = `${this.urlApi}/ativos`;
    return this.http.get<Curso[]>(url, headersOptions)
  }

  AtualizarCurso(curso:Curso): Observable<Curso>
  {
    const url = `${this.urlApi}/${curso.cursoId}`;
    return this.http.put<Curso>(url, curso, headersOptions)
  }

  ExcluirCurso(cursoId: number): Observable<Curso>
  {
    const url = `${this.urlApi}/exclusaologica/${cursoId}`;
    return this.http.put<Curso>(url, headersOptions)
  }
}
