import { Comentario } from "./Comentario";
import { Usuario } from "./Usuario";

export interface Topico {
    id: string;
    titulo: string;
    comentarios: Comentario[];
    usuarioId: string;
    usuario: Usuario;
    horaCriacao: Date;
  }