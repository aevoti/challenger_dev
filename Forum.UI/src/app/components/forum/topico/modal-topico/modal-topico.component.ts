import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Topico } from 'src/app/models/topico';
import { ForumService } from 'src/app/services/forum.service';
import { ModalEditarTopicoComponent } from '../modal-editar-topico/modal-editar-topico.component';
import { Comentario } from 'src/app/models/comentario';
import { ModalEditarComentarioComponent } from '../../comentario/modal-editar-comentario/modal-editar-comentario.component';

@Component({
  selector: 'app-modal-topico',
  templateUrl: './modal-topico.component.html',
  styleUrls: ['./modal-topico.component.css']
})
export class ModalTopicoComponent {

  topico!: Topico;

  dropdownOpen: boolean[] = [];

	constructor(
    public activeModal: NgbActiveModal,
    private modalService: NgbModal,
    private forumService: ForumService
    ) {}

  editarTopico(topico: Topico) {
    const modalRef = this.modalService.open(ModalEditarTopicoComponent, {size: 'xl', scrollable: true, centered: true});
		modalRef.componentInstance.topico = topico;
  }

  excluirTopico(id: number) {
    const confirmacao = confirm('Tem certeza de que deseja excluir este tópico?');
  
    if (confirmacao) {
      this.forumService.deletarTopico(id).subscribe({
        next: (resultado) => {
          if (resultado) {
            console.log(`Tópico com ID ${id} excluído com sucesso.`);
            this.activeModal.close();
          } else {
            console.error(`Erro ao excluir o tópico com ID ${id}.`);
          }
        },
        error: (error) => {
          console.error(`Erro ao excluir o tópico com ID ${id}:`, error);
        }
      });
    }
  }

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