import { Usuario } from "./usuario";

export class Comentario {
  id: number;
  idUsuario: number;
  idTopico: number;
  data: string;
  descricao: string;
  usuario: Usuario;

  constructor(id: number, idUsuario: number, idTopico: number, data: string, descricao: string,usuario: Usuario){
      this.id = id;
      this.idUsuario = idUsuario;
      this.idTopico = idTopico;
      this.data = data;
      this.descricao = descricao;
      this.usuario = usuario;
  }
}
