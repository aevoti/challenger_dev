import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoComponent } from 'src/app/components/modal-confirmacao/modal-confirmacao.component';
import { Comentario } from 'src/app/models/comentario';
import { ForumService } from 'src/app/services/forum.service';

@Component({
  selector: 'app-modal-editar-comentario',
  templateUrl: './modal-editar-comentario.component.html',
  styleUrls: ['./modal-editar-comentario.component.css']
})
export class ModalEditarComentarioComponent {

  comentario!: Comentario;
  topicoId!: number;
  usuarioId!: number;

  conteudoOriginal: string = '';
  conteudoEditavel: string = '';
  caracteresRestantes: number = 0;

  botaoClasse = 'ativo';

  constructor(
    public activeModal: NgbActiveModal,
    private forumService: ForumService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.conteudoOriginal = this.comentario.conteudo;
    this.conteudoEditavel = this.comentario.conteudo;
    this.caracteresRestantes = 2000 - this.conteudoEditavel.length;
  }

  atualizarEValidarComentario(novoComentario: string) {
    this.atualizarClasseBotao(novoComentario);
    this.contadorCaracteres(novoComentario);
  }

  atualizarClasseBotao(novoComentario: string) {
    if (novoComentario.trim() !== '') {
      this.botaoClasse = 'ativo';
    } else {
      this.botaoClasse = 'inativo';
    }
  }

  contadorCaracteres(novoComentario: string) {
    const maxCharacters = 2000;
    const inputLength = novoComentario.length;

    this.caracteresRestantes = maxCharacters - inputLength;
  }

  salvarEdicao() {
    this.conteudoEditavel = this.conteudoEditavel.trim();

    if (this.conteudoEditavel !== '') {
      const modalRef = this.modalService.open(ModalConfirmacaoComponent, { centered: true });
      modalRef.componentInstance.id = this.comentario.id;
      modalRef.componentInstance.tipo = "comentário";
      modalRef.componentInstance.tipoAcao = "atualizar";

      modalRef.result.then((resultado) => {
        if (resultado === 'confirmado') {
          this.comentario.conteudo = this.conteudoEditavel;
          this.forumService.atualizarComentario(this.comentario, this.topicoId, this.usuarioId).subscribe({
            next: (comentarioAtualizado) => {
              console.log(comentarioAtualizado);
              this.activeModal.close();
              this.toastr.success(`Comentário com ID ${this.comentario.id} atualizado com sucesso.`);
            },
            error: (error) => {
              this.toastr.error(`${error.error}`);
              this.comentario.conteudo = this.conteudoOriginal;
            }
          });
        } else {
          this.toastr.info('Edição cancelada pelo usuário.');
        }
      });
    } else {
      this.toastr.error('O campo de edição está vazio ou contém apenas espaços em branco.');
    }
  }
}
