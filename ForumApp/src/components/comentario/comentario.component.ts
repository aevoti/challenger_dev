import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Comentario } from 'src/app/models/comentario';
import { ComentarioBody } from 'src/app/models/comentarioBody';
import { TopicoService } from 'src/app/services/topicoService/topico.service';

@Component({
  selector: 'app-comentario',
  templateUrl: './comentario.component.html',
  styleUrls: ['./comentario.component.scss']
})
export class ComentarioComponent implements OnInit {

  inputValueComentario = "";
  editMode = false;
  editValue = ""

  @Input () comentario!:Comentario
  @Output() onDeleteComentario= new EventEmitter()
  constructor(private topicoService:TopicoService, private toastr: ToastrService) { }

  ngOnInit() {
  }

  toggleEditMode(){
    this.editMode = !this.editMode;
    this.editValue = this.comentario.descricao;
  }

  deletarComentario(){
    this.topicoService.deletarComentario(this.comentario.topicoId, this.comentario.id).subscribe(
      {
        next: () => {
          this.onDeleteComentario.emit()
          this.showSuccess("Comentário deletado")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
    );
  }

  atualizaComentario(){
    const atualizarComentario:ComentarioBody = {
      descricao: this.editValue,
      usuarioId: this.comentario.usuarioId
    }
    this.topicoService.updateComentario(this.comentario.topicoId, this.comentario.id, atualizarComentario).subscribe(
      {
        next: () => {
          this.comentario.descricao = this.editValue
          this.editMode = false
          this.showSuccess("Comentário atualizado")
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
