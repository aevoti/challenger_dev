import { NgModule } from '@angular/core';

import { CadastrarTopicoComponent } from './topico/cadastrar-topico/cadastrar-topico.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ListarTopicosComponent } from './topico/listar-topicos/listar-topicos.component';
import { ModalComentariosComponent } from './comentario/modal-comentarios/modal-comentarios.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalEditarTopicoComponent } from './topico/modal-editar-topico/modal-editar-topico.component';
import { ModalTopicoComponent } from './topico/modal-topico/modal-topico.component';
import { ModalEditarComentarioComponent } from './comentario/modal-editar-comentario/modal-editar-comentario.component';
import { LoginComponent } from './usuario/login/login.component';
import { RegistrarUsuarioComponent } from './usuario/registrar-usuario/registrar-usuario.component';
import { ModalConfirmacaoComponent } from '../modal-confirmacao/modal-confirmacao.component';

@NgModule({
  declarations: [
    CadastrarTopicoComponent,
    ListarTopicosComponent,
    ModalTopicoComponent,
    ModalEditarTopicoComponent,
    ModalComentariosComponent,
    ModalEditarComentarioComponent,
    LoginComponent,
    RegistrarUsuarioComponent,
    ModalConfirmacaoComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbModule,
  ],
  exports:[
    CadastrarTopicoComponent,
    ListarTopicosComponent,
    ModalTopicoComponent,
    ModalEditarTopicoComponent,
    ModalComentariosComponent,
    ModalEditarComentarioComponent,
    LoginComponent,
    RegistrarUsuarioComponent,
    ModalConfirmacaoComponent
  ]
})
export class ForumModule { }
