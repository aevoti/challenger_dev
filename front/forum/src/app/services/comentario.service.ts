import { Injectable } from '@angular/core';
import { Comentario } from 'src/app/models/comentario';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComentarioService {


  constructor(private http: HttpClient) { }

  cadastrarComentario(comentario: Comentario): Observable<any> {
    return this.http.post(`${environment.UrlPrincipal}/comentario/${comentario.idTopico}`, comentario);
  }

  buscarTodosComentariosTopico(idTopico: number): Observable<Comentario[]> {
    return this.http.get<Comentario[]>(`${environment.UrlPrincipal}/comentario/topico/${idTopico}`);
  }

  deletarComentario(id: number): Observable<any> {
    return this.http.delete(`${environment.UrlPrincipal}/comentario/${id}`);
  }

  editarComentario(comentario: Comentario): Observable<any> {
    return this.http.put(`${environment.UrlPrincipal}/comentario/${comentario.id}`, comentario);
  }

}
