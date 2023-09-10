import { Comentario } from "./comentario"

export interface Topico{
    id: number,
    titulo: string,
    descricao: string,
    dataCriacao: string,
    usuarioName: string,
    usuarioPhoto: string,
    usuarioId: number,
    comentarios: Comentario[]
}