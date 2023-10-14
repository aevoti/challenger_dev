import { Injectable } from '@angular/core';
import { Topico } from 'src/app/models/topico';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TopicoService {

  baseUrl = `${environment.UrlPrincipal}/topico`;
  baseUrlForum = `${environment.UrlPrincipal}/forum`;

  constructor(private http: HttpClient) { }

  cadastrarTopico(topico: Topico): Observable<any> {
    return this.http.post(`${environment.UrlPrincipal}/topico`, topico);
  }

  buscarTodosTopicos(): Observable<Topico[]> {
    return this.http.get<Topico[]>(this.baseUrlForum);
  }

  editarTopico(topico: Topico): Observable<any> {
    return this.http.put(`${environment.UrlPrincipal}/topico/${topico.id}`, topico);
  }

  deletarTopico(idTopico: number): Observable<any> {
    return this.http.delete(`${environment.UrlPrincipal}/topico/${idTopico}`);
  }

}
