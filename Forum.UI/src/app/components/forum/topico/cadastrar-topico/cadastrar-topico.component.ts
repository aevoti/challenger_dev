import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Topico } from 'src/app/models/topico';
import { ForumService } from 'src/app/services/forum.service';

@Component({
  selector: 'app-cadastrar-topico',
  templateUrl: './cadastrar-topico.component.html',
  styleUrls: ['./cadastrar-topico.component.css']
})
export class CadastrarTopicoComponent {
  topicos: Topico[] = [];
  pesquisa: string = '';
  ordenacao: string = 'DESC';

  botaoClasse = 'inativo';

  novoTopico: Topico = {
    id: 0,
    usuarioId: 0,
    conteudo: '',
    curtidas: [],
    comentarios: [],
    usuarioNome: '',
    novoComentario: '',
    expandido: false,
    caracteresRestantes: 2000,
    topicoCurtido: false,
    areaComentarioExpandido: false
  };

  tokenUsuarioNome: string | null | undefined;
  tokenUsuarioId: number = 0;

  constructor(
    private forumService: ForumService,
    private jwtHelper: JwtHelperService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const tokenDecodificado = this.jwtHelper.decodeToken(token);
      this.tokenUsuarioNome = tokenDecodificado.nome;
      this.tokenUsuarioId = tokenDecodificado.id;
    } else {
      this.tokenUsuarioNome = null;
      this.tokenUsuarioId = 0;
    }
  }

  obterTopicos() {
    this.forumService.obterTopicos(this.pesquisa, this.ordenacao).subscribe({
      next: (topicos) => {
        this.topicos = topicos;
      },
      error: (error) => {
        console.error('Erro ao obter tópicos:', error);
      }
    });
  }

  atualizarClasseBotao() {
    if (this.novoTopico.conteudo.trim() !== '') {
      this.botaoClasse = 'ativo';
    } else {
      this.botaoClasse = 'inativo';
    }
  }

  cadastrarTopico() {
    if (this.novoTopico.conteudo.trim() !== '') {
      this.novoTopico.usuarioId = this.tokenUsuarioId;
      this.forumService.cadastrarTopico(this.novoTopico).subscribe({
        next: (resultado) => {
          if (resultado) {
            window.location.reload();
          } else {
            this.toastr.error('Erro ao cadastrar o tópico.');
          }
        },
        error: (error) => {
          if (error && error.error) {
            this.toastr.error(`${error.error}`);
          } else {
            this.toastr.error(`Erro ao cadastrar o tópico.`);
          }
        }
      });
      this.novoTopico.conteudo = '';
      this.botaoClasse = 'inativo';
    } else {
      this.toastr.error(`O campo de texto está vazio.`);
    }
  }
}