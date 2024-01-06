import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  usuario: Usuario = {
    id: 0,
    nome: '',
    email: '',
    senha: '',
    foto: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.usuario).subscribe({
      next: (response: any) => {
        const jwtToken = response.token;
        if (jwtToken) {
          localStorage.setItem('token', jwtToken);
          localStorage.setItem('reload', 'true');

          this.router.navigate(['/home']);
        }
      },
      error: (error) => {
        console.error('Erro de login:', error);
      }
    });
  }
}
