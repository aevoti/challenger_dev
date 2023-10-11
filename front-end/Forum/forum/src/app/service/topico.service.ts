import { Injectable } from '@angular/core';
import { TopicosComponent } from '../components/topicos/topicos.component';
import { Topico } from '../Topico';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, finalize } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class TopicoService {
  protected topicoList: Topico[] = [];
  constructor(private http: HttpClient,
     private router: Router) { }

  private apiUrl = 'https://localhost:7299/api/Topicos'

   httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  getTodosTopicos() : Observable<Topico[]> {
    return this.http.get<Topico[]>(this.apiUrl)
  }

  createString(text: string): Observable<string> {
    const requestBody = { text: text };
    const textValue = requestBody.text; // Isso extrairá o valor da propriedade 'text' do objeto
    const textString = JSON.stringify(textValue);
    
    console.log(textValue)
    const apiUrlPost = 'https://localhost:7299/api/Topicos?text=';
    return this.http.post<string>(apiUrlPost+textString, this.httpOptions)
    .pipe(
      // Assim que a solicitação for concluída, navegue de volta à tela principal
      finalize(() => {
        window.location.reload(); // Defina a rota da tela principal conforme necessário
      })
    );
    
  }

  getTopicoPorText(texto: string) : Observable<Topico[]> {
    return this.http.get<Topico[]>(this.apiUrl+texto,this.httpOptions)
    
  }

  getTopicoPorData(texto: string) : Observable<Topico[]> {
    const params = new HttpParams()
      .set('ordem', texto)
    return this.http.get<Topico[]>(this.apiUrl, { params });
  }
  getTopicoPorDataFiltro(texto: string, filtro: string) : Observable<Topico[]> {
    const params = new HttpParams()
      .set('ordem', texto)
      .set('titulo',filtro )
    return this.http.get<Topico[]>(this.apiUrl, { params });
  }
}
