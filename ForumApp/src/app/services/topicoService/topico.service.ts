import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CriarTopicoBody } from 'src/app/models/criarTopicoBody';
import { Topico } from 'src/app/models/topico';
import { enviroment } from 'src/enviroments/enviroment';

@Injectable()
export class TopicoService {

    constructor(private http:HttpClient) { }

    getTopicos(order:string = "Crescente", searchText:string):Observable<Topico[]>{
        return this.http.get<Topico[]>(`${enviroment.APIURL}forum`, 
            {params:{order, searchText}})
    }

    createTopico(body:CriarTopicoBody):Observable<Topico>{
        return this.http.post<Topico>(`${enviroment.APIURL}topico`, 
            body)
    }
}
