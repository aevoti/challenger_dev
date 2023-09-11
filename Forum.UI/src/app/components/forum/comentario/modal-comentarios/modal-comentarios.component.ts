import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Comentario } from 'src/app/models/comentario';
import { ForumService } from 'src/app/services/forum.service';
import { ModalEditarComentarioComponent } from '../modal-editar-comentario/modal-editar-comentario.component';

@Component({
  selector: 'app-modal-comentarios',
  templateUrl: './modal-comentarios.component.html',
  styleUrls: ['./modal-comentarios.component.css']
})
export class ModalComentariosComponent {
  
  comentarios: Comentario[] = [];

  dropdownOpen: boolean[] = [];

	constructor(
    public activeModal: NgbActiveModal,
    private modalService: NgbModal,
    private forumService: ForumService
    ) {}

  toggleDropdown(index: number) {
    this.dropdownOpen[index] = !this.dropdownOpen[index];
  }

  editarComentario(comentario: Comentario, topicoId: number, usuarioId: number) {
    const modalRef = this.modalService.open(ModalEditarComentarioComponent, {size: 'xl', scrollable: true, centered: true});
		modalRef.componentInstance.comentario = comentario;
		modalRef.componentInstance.topicoId = topicoId;
		modalRef.componentInstance.usuarioId = usuarioId;
  }

  excluirComentario(comentario: Comentario, topicoId: number, usuarioId: number) {
    const confirmacao = confirm('Tem certeza de que deseja excluir este comentário?');
  
    if (confirmacao) {
      this.forumService.deletarComentario(comentario, topicoId, usuarioId).subscribe({
        next: (resultado) => {
          if (resultado) {
            console.log(`Comentário com ID ${comentario.id} excluído com sucesso.`);
            this.activeModal.close();
          } else {
            console.error(`Erro ao excluir o comentário com ID ${comentario.id}.`);
          }
        },
        error: (error) => {
          console.error(`Erro ao excluir o comentário com ID ${comentario.id}:`, error);
        }
      });
    }
  }
}

