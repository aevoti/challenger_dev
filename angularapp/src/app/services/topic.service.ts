import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Topic } from '../models/topic.model'; 
import { Api } from '../config/api';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TopicService {
  

  constructor(private http: HttpClient) { }

  getTopics(): Observable<Topic[]> {
    const url = `${Api.url()}/api/forum`;
    return this.http.get<Topic[]>(`${url}`).pipe(
      catchError(error => {
        console.error('Erro na solicitação HTTP:', error);
        // Trate o erro de acordo com a sua necessidade, por exemplo, exibindo uma mensagem de erro para o usuário.
        return throwError('Erro ao buscar tópicos. Por favor, tente novamente mais tarde.');
      })
    );
  }

  getTopicById(topicId: number): Observable<Topic> {
    // Construa a URL com o ID do tópico
    const url = `${Api.url()}/api/topico/${topicId}`;

    // Faça a solicitação HTTP GET para buscar os comentários
    return this.http.get<Topic>(url);
  }
  createTopic(newTopic: Topic): Observable<Topic> {
    const url = `${Api.url()}/api/topico`;
    return this.http.post<Topic>(url, newTopic);
  }

  deleteTopic(topicId: number): Observable<void> {
    const url = `${Api.url()}/api/topico/${topicId}`;
    return this.http.delete<void>(url);
  }

  updateTopic(updatedTopic: Topic): Observable<Topic> {
    const url = `${Api.url()}/api/topico/${updatedTopic.id}`;
    return this.http.put<Topic>(url, updatedTopic);
  }

  deleteComment(topicId: number, commentId: number ): Observable<void> {
    const url = `${Api.url()}/api/comentario/${topicId}/${commentId}`;
    return this.http.delete<void>(url);
  }

  createComment(topicId: number, comment: string): Observable<Comment> {
    const url = `${Api.url()}/api/comentario/${topicId}`;
    return this.http.post<Comment>(url, { comment });
  }
}
