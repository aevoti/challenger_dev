import { Comentario } from "./comentario";
import { Curtida } from "./curtida";

export interface Topico {
    id: number;
    dataHoraPostagem?: Date;
    usuarioId: number;
    conteudo: string;
    curtidas: Curtida[];
    comentarios: Comentario[];

    novoComentario: string;
    usuarioNome: string;
    expandido: boolean;
    caracteresRestantes: number;
    topicoCurtido: boolean;
    areaComentarioExpandido: boolean;
}