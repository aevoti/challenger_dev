import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Comentario } from 'src/app/models/comentario';
import { Topico } from 'src/app/models/topico';
import { ForumService } from 'src/app/services/forum.service';
import { ModalComentariosComponent } from '../../comentario/modal-comentarios/modal-comentarios.component';
import { ModalEditarTopicoComponent } from '../modal-editar-topico/modal-editar-topico.component';
import { ModalEditarComentarioComponent } from '../../comentario/modal-editar-comentario/modal-editar-comentario.component';
import { ModalTopicoComponent } from '../modal-topico/modal-topico.component';
import { Curtida } from 'src/app/models/curtida';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { ToastrService } from 'ngx-toastr';
import { ModalConfirmacaoComponent } from 'src/app/components/modal-confirmacao/modal-confirmacao.component';

@Component({
  selector: 'app-listar-topicos',
  templateUrl: './listar-topicos.component.html',
  styleUrls: ['./listar-topicos.component.css'],
})
export class ListarTopicosComponent {
  topicos: Topico[] = [];
  pesquisa: string = '';
  ordenacao: string = 'DESC';

  botaoClasse = 'inativo';

  novoTopico: Topico = {
    id: 0,
    usuarioId: 1,
    conteudo: '',
    comentarios: [],
    curtidas: [],
    usuarioNome: '',
    novoComentario: '',
    expandido: false,
    caracteresRestantes: 2000,
    topicoCurtido: false,
    areaComentarioExpandido: false
  };

  novoComentario: Comentario = {
    id: 0,
    topicoId: 0,
    usuarioId: 0,
    usuarioNome: '',
    conteudo: '',
  }

  tokenUsuarioNome: string | null | undefined;
  tokenUsuarioId: number = 0;

  constructor(
    private forumService: ForumService,
    private modalService: NgbModal,
    private authService: AuthService,
    private jwtHelper: JwtHelperService,
    private toastr: ToastrService
  ) {
    this.topicos.forEach((topico) => {
      topico.expandido = false;
    });
  }

  ngOnInit(): void {
    this.verificarTokens();
    this.obterTopicos();
  }

  verificarTokens() {
    let reload = localStorage.getItem('reload');
    if (reload === 'true') {
      localStorage.setItem('reload', 'false');
      window.location.reload();
    }

    let token = localStorage.getItem('token');
    if (token) {
      let tokenDecodificado = this.jwtHelper.decodeToken(token);
      this.tokenUsuarioNome = tokenDecodificado.nome;
      this.tokenUsuarioId = tokenDecodificado.id;
    } else {
      this.tokenUsuarioNome = null;
      this.tokenUsuarioId = 0;
    }
  }

  obterTopicos(): void {
    this.forumService.obterTopicos(this.pesquisa, this.ordenacao).subscribe({
      next: (topicos) => {
        topicos.forEach((topico) => {
          this.authService.obterUsuarioPorId(topico.usuarioId).subscribe((usuario) => {
            topico.usuarioNome = usuario.nome;

            topico.comentarios.forEach((comentario) => {
              this.authService.obterUsuarioPorId(comentario.usuarioId).subscribe((comentarioUsuario) => {
                comentario.usuarioNome = comentarioUsuario.nome;
              });
            });
            topico.caracteresRestantes = 2000;
          });
        });
        this.verificarCurtida(topicos);
        this.topicos = topicos;
        this.verificarComentarioExpandido();
      },
      error: (error) => {
        if (error && error.error) {
          this.toastr.info(`${error.error}`);
        }
      }
    });
  }

  verificarCurtida(topicos: Topico[]) {
    topicos.forEach(topico => {
      const usuarioJaCurtiu = topico.curtidas.some(curta => curta.usuarioId == this.tokenUsuarioId);
      if (usuarioJaCurtiu) {
        topico.topicoCurtido = true;
      }
    });
  }

  verificarComentarioExpandido() {
    this.topicos.forEach((topico) => {
      const storedAreaComentarioExpandido = localStorage.getItem(`areaComentarioExpandido_${topico.id}`);
      if (storedAreaComentarioExpandido) {
        topico.areaComentarioExpandido = storedAreaComentarioExpandido === 'true';
      }
    });
  }

  cortarTexto(texto: string, limite: number): string {
    if (texto.length <= limite) {
      return texto;
    }
    const ultimaPalavraIndex = texto.lastIndexOf(' ', limite);
    return texto.substring(0, ultimaPalavraIndex) + '...';
  }

  obterTopicoPorId(topico: Topico) {
    const modalRef = this.modalService.open(ModalTopicoComponent, { size: 'xl', scrollable: true, centered: true });
    modalRef.componentInstance.topico = topico;
  }

  editarTopico(topico: Topico) {
    const modalRef = this.modalService.open(ModalEditarTopicoComponent, { size: 'xl', scrollable: true, centered: true });
    modalRef.componentInstance.topico = topico;
  }

  excluirTopico(id: number) {
    const modalRef = this.modalService.open(ModalConfirmacaoComponent, { centered: true });
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.tipo = "tópico";
    modalRef.componentInstance.tipoAcao = "excluir";

    modalRef.result.then((resultado) => {
      if (resultado === 'confirmado') {
        this.forumService.deletarTopico(id).subscribe({
          next: (resultado) => {
            if (resultado) {
              this.toastr.success(`Tópico com ID ${id} excluído com sucesso.`);
              this.obterTopicos();
            } else {
              this.toastr.error('Erro ao excluir o tópico.');
            }
          },
          error: (error) => {
            if (error && error.error) {
              this.toastr.error(`${error.error}`);
            } else {
              this.toastr.error(`Erro ao excluir o tópico com ID ${id}.`);
            }
          }
        });
      }
    });
  }

  curtirOuDescurtirTopico(topico: Topico) {
    const curtida: Curtida = {
      id: 0,
      topicoId: topico.id,
      usuarioId: this.tokenUsuarioId
    };

    this.forumService.curtirOuDescurtirTopico(curtida).subscribe((curtiu) => {
      if (curtiu) {
        topico.topicoCurtido = true;
      } else {
        topico.topicoCurtido = false;
      }
      this.obterTopicos();
    });
  }

  toggleComentario(topico: Topico) {
    topico.areaComentarioExpandido = !topico.areaComentarioExpandido;
    console.log(topico.areaComentarioExpandido);

    localStorage.setItem(`areaComentarioExpandido_${topico.id}`, topico.areaComentarioExpandido.toString());
  }

  cadastrarComentario(topicoId: number, novoComentario: string = '') {
    if (novoComentario.trim() !== '') {
      this.novoComentario.conteudo = novoComentario;
      this.novoComentario.usuarioId = this.tokenUsuarioId;

      this.forumService.cadastrarComentario(this.novoComentario, topicoId).subscribe({
        next: (novoComentario: Comentario) => {
          console.log('Comentário cadastrado:', novoComentario);
          this.obterTopicos();
        },
        error: (error) => {
          if (error && error.error) {
            this.toastr.error(`${error.error}`);
          } else {
            this.toastr.error('Erro ao cadastrar o comentário.');
          }
        }
      });
    } else {
      this.toastr.error('O campo de comentário está vazio.');
    }
  }

  editarComentario(comentario: Comentario, topicoId: number, usuarioId: number) {
    const modalRef = this.modalService.open(ModalEditarComentarioComponent, { size: 'xl', scrollable: true, centered: true });
    modalRef.componentInstance.comentario = comentario;
    modalRef.componentInstance.topicoId = topicoId;
    modalRef.componentInstance.usuarioId = usuarioId;
  }

  excluirComentario(comentario: Comentario, topicoId: number, usuarioId: number) {
    const modalRef = this.modalService.open(ModalConfirmacaoComponent, { centered: true });
    modalRef.componentInstance.id = comentario.id;
    modalRef.componentInstance.tipo = "comentário";
    modalRef.componentInstance.tipoAcao = "excluir";

    modalRef.result.then((resultado) => {
      if (resultado === 'confirmado') {
        this.forumService.deletarComentario(comentario, topicoId, usuarioId).subscribe({
          next: (resultado) => {
            if (resultado) {
              this.toastr.success(`Comentário com ID ${comentario.id} excluído com sucesso.`);
              this.obterTopicos();
            } else {
              this.toastr.error(`Erro ao excluir o comentário com ID ${comentario.id}.`);
            }
          },
          error: (error) => {
            if (error && error.error) {
              this.toastr.error(`${error.error}`);
            } else {
              this.toastr.error(`Erro ao excluir o comentário com ID ${comentario.id}.`);
            }
          }
        });
      }
    });
  }

  obterComentariosPorTopico(comentarios: Comentario[]) {
    const modalRef = this.modalService.open(ModalComentariosComponent, { size: 'xl', scrollable: true, centered: true });
    modalRef.componentInstance.comentarios = comentarios;
  }

  atualizarEValidarComentario(topico: Topico) {
    this.atualizarClasseBotao(topico.novoComentario);
    this.contadorCaracteres(topico);
  }

  atualizarClasseBotao(novoComentario: string) {
    if (novoComentario.trim() !== '') {
      this.botaoClasse = 'ativo';
    } else {
      this.botaoClasse = 'inativo';
    }
  }

  contadorCaracteres(topico: Topico) {
    const maxCharacters = 2000;
    const inputLength = topico.novoComentario.length;

    topico.caracteresRestantes = maxCharacters - inputLength;
  }
}