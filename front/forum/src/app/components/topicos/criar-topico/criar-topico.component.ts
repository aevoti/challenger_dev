import { Component } from '@angular/core';
import { Topico } from 'src/app/models/topico';
import { Usuario } from 'src/app/models/usuario';
import { TopicoService } from 'src/app/services/topico.service';

@Component({
  selector: 'app-criar-topico',
  templateUrl: './criar-topico.component.html',
  styleUrls: ['./criar-topico.component.css']
})
export class CriarTopicoComponent {

  descricao: string = "";
  topico: Topico = {
    id:1,
    data: "2023-09-10T00:42:02.005Z",
    idUsuario: 1,
    descricao: "",
    comentarios: [],
    usuario: {
      id: 1,
      nome: "Thayna",
      email: "thaynavaladaresclebio@gmail.com",
      foto: ""
    }
  }

  constructor(
    private topicoService: TopicoService
  ) { }

  ngOnInit(): void {
  }

  criarTopico() {
    this.topico.descricao = this.descricao;
    this.topicoService.cadastrarTopico(this.topico).subscribe(() => {
    })
    alert("Tópico criado!!")
    location.reload();
  }
}
