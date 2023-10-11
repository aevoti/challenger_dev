import { Component } from '@angular/core';
import { Route, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent {
  usuario = {
    nome: '',
    email : '',
    senha: '',
    foto: null
  };

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.usuario.foto = file;
    }
  }

  onSubmit() {
    // Aqui você pode enviar os dados do cliente (nome e foto) para o seu servidor ou fazer o que for necessário.
    console.log(this.usuario);
  }
}