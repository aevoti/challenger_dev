import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Comentario } from '../Comentario';
import { Observable, finalize } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComentarioService {
  protected comentarioList: Comentario[] = [];
  constructor(private http: HttpClient) { }

  private apiUrl = 'https://localhost:7299/api/Comentario/'

  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Headers':
        'Origin, Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
      'Content-Type': 'application/json; charset=UTF-8',
    }),
  };
  getTodosComentarios(id: string) : Observable<Comentario[]> {
    
    return this.http.get<Comentario[]>(this.apiUrl + id,this.httpOptions)
  }


  createString(text: string, topicoId : string): Observable<string> {
    const requestBody = { text: text };
    const textValue = requestBody.text; // Isso extrairá o valor da propriedade 'text' do objeto
    const topicoString = JSON.stringify(topicoId);
    
    const dados = {comentario : requestBody.text}
    console.log(topicoString)
    const apiUrlPost = 'https://localhost:7299/api/Comentario';
    const url = `${this.apiUrl}/${topicoString}`
    return this.http.post<string>(url, dados)
    // .pipe(
    //   // Assim que a solicitação for concluída, navegue de volta à tela principal
    //   finalize(() => {
    //     window.location.reload(); // Defina a rota da tela principal conforme necessário
    //   })
    // );
    
  }
}
