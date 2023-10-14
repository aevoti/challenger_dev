import { Component } from '@angular/core';
import { Topico } from 'src/app/models/topico';
import { TopicoService } from 'src/app/services/topico.service';
import { ComentarioService } from 'src/app/services/comentario.service';
import { Comentario } from 'src/app/models/comentario';
import { Usuario } from 'src/app/models/usuario';

@Component({
  selector: 'app-listar-topico',
  templateUrl: './listar-topico.component.html',
  styleUrls: ['./listar-topico.component.css']
})
export class ListarTopicoComponent {

  ordenacao: string = "desc";
  topicos!: Topico[];
  descricao: string = "";
  topic: Topico = {
    id:1,
    data: "2023-09-10T00:42:02.005Z",
    idUsuario: 2,
    descricao: "",
    comentarios: [],
    usuario: {
      id: 1,
      nome: "Thayna",
      email: "thaynavaladaresclebio@gmail.com",
      foto: ""
    }
  }
  usuarios: Usuario[] = [
    {
      id:1,
      nome: "Thayna Valadares",
      email: "thaynavaladaresclebio@gmail.com",
      foto: "../../../assets/images/flor.jpg"
    },
    {
      id:2,
      nome: "Ana Souza",
      email: "anasouza@gmail.com",
      foto: "../../../assets/images/lua.jpg"
    },
    {
      id:3,
      nome: "Joaquim Amaral",
      email: "joaquimamaral@gmail.com",
      foto: "../../../assets/images/onca.jpg"
    },
    {
      id:4,
      nome: "Pedro Soares",
      email: "pedrosoares@gmail.com",
      foto: "../../../assets/images/lua.jpg"
    },
    {
      id:5,
      nome: "Natalia Silva",
      email: "nathaliasilva@gmail.com",
      foto: "../../../assets/images/onca.jpg"
    }
  ]
  comentario: Comentario = {
    id:1,
    data: "2023-09-10T00:42:02.005Z",
    idUsuario: 1,
    idTopico: 1,
    descricao: "",
    usuario: {
      id: 1,
      nome: "Thayna",
      email: "thaynavaladaresclebio@gmail.com",
      foto: ""
    }
  }

  constructor(
    private topicoService: TopicoService,
    private comentarioService: ComentarioService
  ) { }

  ngOnInit() {
    this.carregarTopicos();
  }

  carregarTopicos() {
    this.topicoService.buscarTodosTopicos().subscribe(
      (topicos: Topico[]) => {
        this.topicos = this.filtragemTopico(topicos);
        this.topicos.forEach( (value) => {
          this.usuarios.forEach( (usuario) => {
            if(usuario.id == value.idUsuario) value.usuario = usuario
          });

          this.comentarioService.buscarTodosComentariosTopico(value.id).subscribe(
            (comentarios: Comentario[]) => {
              value.comentarios = comentarios;
              comentarios.forEach( (comentario) => {
                this.usuarios.forEach( (usuario) => {
                  if(usuario.id == comentario.idUsuario) comentario.usuario = usuario
                });
              });

            }
          );
        });
      }
    );
  }

  excluirTopico(id:number) {
    this.topicoService.deletarTopico(id).subscribe(() => {
    })
    alert("Tópico deletado!!")
    this.limparFiltros();
  }

  abrirTopico(id:number) {
    var element = document.getElementById(id.toString());
    if(element) element.classList.remove("hidden");
  }

  editarTopico(text:string, id:number) {
    this.topic.id = id;
    this.topic.descricao = text;
    this.topic.idUsuario = 3;
    this.topicoService.editarTopico(this.topic).subscribe(() => {
    })
    alert("Tópico editado!!")
    this.limparFiltros();
  }

  criarComentario(text:string, id: number) {
    this.comentario.idTopico = id;
    this.comentario.descricao = text;
    console.log(this.comentario)
    this.comentarioService.cadastrarComentario(this.comentario).subscribe(() => {
    })
    alert("Comentário criado!!")
    this.limparFiltros();
  }

  excluirComentario(id:number) {
    this.comentarioService.deletarComentario(id).subscribe(() => {
    })
    alert("Comentário deletado!!")
    this.limparFiltros();
  }

  editarComentario(text:string, id:number, idTopico: number) {
    this.comentario.id = id;
    this.comentario.descricao = text;
    this.comentario.idTopico = idTopico;
    this.comentario.idUsuario = 5;
    this.comentarioService.editarComentario(this.comentario).subscribe(() => {
    })
    alert("Comentário editado!!")
    this.limparFiltros();
  }

  abrirComentario(id:number) {
    console.log(id)
    var element = document.getElementById("comentario"+id.toString());
    console.log(element)
    if(element) element.classList.remove("hidden");
  }

  mudarOrdenacao(tipo: string) {
    localStorage.setItem('ordenacao', tipo);
    this.limparFiltros();
  }

  filtragemTopico(topicos: Topico[]){
    let buscaTopico:string = localStorage.getItem('topico') || '';
    let ordenar:string = localStorage.getItem('ordenacao') || '';

    topicos = topicos.filter(el => el.descricao.includes(buscaTopico.toString()));

    if(ordenar == "cresc") {
      this.ordenacao = "cresc";
      topicos.sort((a, b) => (a.data < b.data ? -1 : 1))
    } else {
      topicos.sort((a, b) => (a.data > b.data ? -1 : 1))
    }
    return topicos;
  }

  limparFiltros(){
    localStorage.setItem('topico', "");
    location.reload();
  }
}

