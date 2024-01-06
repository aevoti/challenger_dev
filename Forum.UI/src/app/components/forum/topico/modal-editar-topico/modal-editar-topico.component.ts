import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoComponent } from 'src/app/components/modal-confirmacao/modal-confirmacao.component';
import { Topico } from 'src/app/models/topico';
import { ForumService } from 'src/app/services/forum.service';

@Component({
  selector: 'app-modal-editar-topico',
  templateUrl: './modal-editar-topico.component.html',
  styleUrls: ['./modal-editar-topico.component.css']
})
export class ModalEditarTopicoComponent {

  topico!: Topico;
  conteudoEditavel: string = '';

  conteudoOriginal: string = '';

  botaoClasse = 'ativo';

  constructor(
    public activeModal: NgbActiveModal,
    private forumService: ForumService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.conteudoOriginal = this.topico.conteudo;
    this.conteudoEditavel = this.topico.conteudo;
  }

  atualizarClasseBotao(novoComentario: string) {
    if (novoComentario.trim() !== '') {
      this.botaoClasse = 'ativo';
    } else {
      this.botaoClasse = 'inativo';
    }
  }

  salvarEdicao() {
    this.conteudoEditavel = this.conteudoEditavel.trim();

    if (this.conteudoEditavel !== '') {
      const modalRef = this.modalService.open(ModalConfirmacaoComponent, { centered: true });
      modalRef.componentInstance.id = this.topico.id;
      modalRef.componentInstance.tipo = "tópico";
      modalRef.componentInstance.tipoAcao = "atualizar";

      modalRef.result.then((resultado) => {
        if (resultado === 'confirmado') {
          this.topico.conteudo = this.conteudoEditavel;
          this.forumService.atualizarTopico(this.topico, this.topico.id).subscribe({
            next: (topicoAtualizado) => {
              console.log(topicoAtualizado);
              this.activeModal.close();
              this.toastr.success(`Tópico com ID ${this.topico.id} atualizado com sucesso.`);
            },
            error: (error) => {
              this.toastr.error(`${error.error}`);
              this.topico.conteudo = this.conteudoOriginal;
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
