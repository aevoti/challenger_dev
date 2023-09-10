import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comentario } from 'src/app/models/comentario';
import { ComentarioBody } from 'src/app/models/comentarioBody';
import { TopicoBody } from 'src/app/models/topicoBody';
import { Topico } from 'src/app/models/topico';
import { enviroment } from 'src/enviroments/enviroment';

@Injectable()
export class TopicoService {

    constructor(private http:HttpClient) { }

    getTopicos(order:string = "Crescente", searchText:string):Observable<Topico[]>{
        return this.http.get<Topico[]>(`${enviroment.APIURL}forum`, 
            {params:{order, searchText}})
    }

    getTopico(idTopico:number):Observable<Topico>{
        return this.http.get<Topico>(`${enviroment.APIURL}topico/${idTopico}`)
    }

    createTopico(body:TopicoBody):Observable<Topico>{
        return this.http.post<Topico>(`${enviroment.APIURL}topico`, body)
    }

    createComentario(idTopico:number, body:ComentarioBody):Observable<Comentario>{
        return this.http.post<Comentario>(`${enviroment.APIURL}comentario/${idTopico}`, body)
    }

    updateTopico(idTopico:number, body:TopicoBody):Observable<void>{
        return this.http.put<void>(`${enviroment.APIURL}topico/${idTopico}`, body)
    }

    deletarTopico(idTopico:number):Observable<void>{
        return this.http.delete<void>(`${enviroment.APIURL}topico/${idTopico}`)
    }

    updateComentario(idTopico:number, idComentario:number, body:ComentarioBody):Observable<void>{
        return this.http.put<void>(`${enviroment.APIURL}comentario/${idTopico}/${idComentario}`, body)
    }

    deletarComentario(idTopico:number, idComentario:number):Observable<void>{
        return this.http.delete<void>(`${enviroment.APIURL}comentario/${idTopico}/${idComentario}`)
    }
}
