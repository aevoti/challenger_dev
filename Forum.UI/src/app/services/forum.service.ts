import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Topico } from '../models/topico';
import { Comentario } from '../models/comentario';
import { Curtida } from '../models/curtida';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  public obterTopicos(filtroTexto: string, ordenacao: string): Observable<Topico[]> {
    let params = new HttpParams();
    if (filtroTexto) {
      params = params.append('filtroTexto', filtroTexto);
    }
    if (ordenacao) {
      params = params.append('ordenacao', ordenacao);
    }

    return this.http.get<Topico[]>(`${environment.apiUrl}/forum`, { params });
  }

  public obterTopicoPorId(id: number) : Observable<Topico> {
    return this.http.get<Topico>(`${environment.apiUrl}/topico/${id}`);
  }

  public cadastrarTopico(topico: Topico) : Observable<Topico> {
    return this.http.post<Topico>(`${environment.apiUrl}/topico`, topico);
  }

  public atualizarTopico(topico: Topico, id: number) : Observable<Topico> {
    return this.http.put<Topico>(`${environment.apiUrl}/topico/${id}`, topico);
  }

  public deletarTopico(id: number) : Observable<boolean> {
    return this.http.delete<boolean>(`${environment.apiUrl}/topico/${id}`);
  }

  public curtirOuDescurtirTopico(curtida: Curtida) : Observable<boolean> {
    return this.http.post<boolean>(`${environment.apiUrl}/topico/curtir`, curtida);
  }

  public cadastrarComentario(comentario: Comentario, topicoId: number) : Observable<Comentario> {
    return this.http.post<Comentario>(`${environment.apiUrl}/comentario/${topicoId}`, comentario);
  }

  public atualizarComentario(comentario: Comentario, topicoId: number, usuarioId: number) : Observable<Topico> {
    return this.http.put<Topico>(`${environment.apiUrl}/comentario/${topicoId}/${usuarioId}`, comentario);
  }

  public deletarComentario(comentario: Comentario, topicoId: number, usuarioId: number) : Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), body: comentario
    };

    return this.http.delete<boolean>(`${environment.apiUrl}/comentario/${topicoId}/${usuarioId}`, httpOptions);
  }
}