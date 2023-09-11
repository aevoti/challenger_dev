import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { comentario, topicos } from './models';
@Injectable({
    providedIn: 'root'
  })

export class ForumService {
    private apiPath : string = 'https://localhost:7145/api/' 
    
    constructor(
        private http: HttpClient
    ) { }

    public getTopicos(ordenacaoSelecionada:number):Observable<topicos[]>{
        return this.http.get<any>(this.apiPath + 'topico/' + ordenacaoSelecionada.toString());
    }

    public pesquisarTopico(dscTopic : string,ordenacaoSelecionada:number):Observable<topicos[]>{
        return this.http.get<any>(this.apiPath + 'topico/' + dscTopic +'/'+ ordenacaoSelecionada.toString());
    }

    public postTopico(dscTopic : string):Observable<any>{
        return this.http.post<any>(this.apiPath + 'topico/',{'Dsc':dscTopic});
    }
    public postComent(idTopico :number,dsc:string):Observable<any>{
        console.log(this.apiPath + 'comentario/', idTopico);
        return this.http.post<any>(this.apiPath + 'comentario/' + idTopico.toString(),{'Dsc':dsc});
    }

    public getComent(idTopic:number):Observable<comentario[]>{
        return this.http.get<any>(this.apiPath + 'comentario/' + idTopic);
    }

    public updateTopico(idTopico :number,dsc:string):Observable<any>{
        return this.http.put<any>(this.apiPath + 'topico/' + idTopico.toString(),{'Dsc':dsc});
    }

    public deleteTopico(idTopico :number):Observable<any>{
        return this.http.delete<any>(this.apiPath + 'topico/' + idTopico.toString());
    }

    public updateComent(idTopico :number,id:number,dsc:string):Observable<any>{
        return this.http.put<any>(this.apiPath + 'comentario/' + idTopico.toString()+'/'+id.toString(),{'Dsc':dsc});
    }

    public deleteComent(idTopico :number,id:number):Observable<any>{
        return this.http.delete<any>(this.apiPath + 'comentario/'+ idTopico.toString()+'/'+id.toString());
    }
}
