import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Comentario } from 'src/app/models/comentario';
import { ComentarioBody } from 'src/app/models/comentarioBody';
import { Topico } from 'src/app/models/topico';
import { TopicoBody } from 'src/app/models/topicoBody';
import { TopicoService } from 'src/app/services/topicoService/topico.service';

@Component({
  selector: 'app-topico',
  templateUrl: './topico.component.html',
  styleUrls: ['./topico.component.scss']
})
export class TopicoComponent implements OnInit {

  toggled = true;
  toggledVerComentarios = false;
  inputValueComentario = "";
  editMode = false;
  editValue = ""

  @Input () topico!:Topico
  @Output() onDelete= new EventEmitter()
  constructor(private topicoService:TopicoService, private toastr: ToastrService) { }

  ngOnInit() {
  }

  curtir(){

  }

  toggleText(){
    this.toggled = !this.toggled;
  }

  toggleVerComentario(){
    this.toggledVerComentarios = !this.toggledVerComentarios;
  }

  toggleEditMode(){
    this.editMode = !this.editMode;
    this.editValue = this.topico.descricao;
  }

  recarregarTopico() {
    this.topicoService.getTopico(this.topico.id).subscribe(topico => {
      this.topico = topico
    });
  }

  retornarComentarios():Comentario[]{
    if(!this.toggledVerComentarios){
      return [this.topico.comentarios[0] || []]
    }
    return this.topico.comentarios
  }

  ComentarioBody(){
    const novoComentario:ComentarioBody = {
      descricao:this.inputValueComentario,
      usuarioId: 1
    }
    this.topicoService.createComentario(this.topico.id, novoComentario).subscribe(
      {
        next: (comentario) => {
          comentario.usuarioPhoto = this.topico.usuarioPhoto
          this.topico.comentarios.push(comentario)
          this.inputValueComentario = ""
          this.showSuccess("Comentário criado")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
    );
  }

  deletarTopico(){
    this.topicoService.deletarTopico(this.topico.id).subscribe(
      {
        next: () => {
          this.onDelete.emit()
          this.showSuccess("Tópico deletado")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
    );
  }

  atualizaTopico(){
    const atualizarTopico:TopicoBody = {
      titulo: this.topico.titulo,
      descricao: this.editValue,
      usuarioId: this.topico.usuarioId
    }
    this.topicoService.updateTopico(this.topico.id, atualizarTopico).subscribe(
      {
        next: () => {
          this.topico.descricao = this.editValue
      this.editMode = false
          this.showSuccess("Tópico atualizado")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
    );
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Sucesso');
  }

  showError(message: string) {
    this.toastr.error(message, 'Erro');
  }
}
