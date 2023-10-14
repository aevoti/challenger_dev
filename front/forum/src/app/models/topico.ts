import { Comentario } from "./comentario";
import { Usuario } from "./usuario";

export class Topico {
  id: number;
  idUsuario: number;
  data: string;
  descricao: string;
  comentarios: Comentario[];
  usuario: Usuario;

  constructor(id: number, idUsuario: number, data: string, descricao: string, comentarios: Comentario[], usuario: Usuario){
      this.id = id;
      this.idUsuario = idUsuario;
      this.data = data;
      this.descricao = descricao;
      this.comentarios = comentarios;
      this.usuario = usuario;
  }
}
