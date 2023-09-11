import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from 'src/app/models/usuario';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  obterUsuarioPorId(usuarioId: number): Observable<Usuario> {
    return this.http.get<Usuario>(`${environment.apiUrl}/auth/obterUsuario/${usuarioId}`);
  }

  registrarUsuario(usuario: any): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/auth/registro`, usuario);
  }

  login(usuario: any): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/auth/login`, usuario);
  }
}
