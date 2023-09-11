import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/app/models/usuario';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent {
  usuario: Usuario = {
    id: 0,
    nome: '',
    email: '',
    senha: '',
    foto: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
    ) { }

  registreLogue() {
    this.authService.registrarUsuario(this.usuario).subscribe({
      next: (response: any) => {
        const jwtToken = response.token;
        if (jwtToken) {
          this.toastr.success(`Usuário criado com sucesso, favor realizar login`);
          this.router.navigate(['/login']);
        }
      },
      error: (error) => {
        console.error('Erro de registro:', error);
      }
    });
  }
}
