import { Component, Input } from '@angular/core';
import { Comentario } from 'src/app/Comentario';
import { ComentarioService } from 'src/app/service/comentario.service';

@Component({
  selector: 'app-comentarios',
  templateUrl: './comentarios.component.html',
  styleUrls: ['./comentarios.component.scss']
})
export class ComentariosComponent {
  comentarios : Comentario[] = [];
  @Input() topicoId: string;
  idTopico :string ='';
  


  constructor(private comentarioService : ComentarioService) {
    this.getComentarios()
    this.topicoId='';
    
  }

  textareaValue = '';
  searchValue = '';
  comentarioValue: string = '';
  text='' ;

  ngOnInit(): void {
    // Coloque aqui o código para fazer a solicitação GET
    this.getComentarios();
  }

  getComentarios(): void {
    // Certifique-se de que o topicoId não esteja vazio antes de fazer a chamada HTTP
    if (this.topicoId) {
      this.comentarioService.getTodosComentarios(this.topicoId).subscribe((comentarios) => {
        this.comentarios = comentarios;
      });
    }
  }

  abrirFormularioDeComentario(topicoId: string) {
    this.idTopico = topicoId;
    
  }

  isTextareaEmpty() {
    return this.searchValue.trim() === '';
  }

  isComentarioEmpty() {
    return this.comentarioValue.trim() === '';
  }

  onComentarioCreate(formValue: any) {
    const comentarioText = formValue.text;
    const topicoId = formValue.topicoId;
     this.comentarioService.createString(comentarioText, topicoId).subscribe((topicos) => (this.text = topicos));
  }

  editarComentario(comentarioId: string, topicoId: string) {
    console.log('Tópico ID:', topicoId);
    const comentario = this.comentarios.find(c => c.id === comentarioId);
    if (comentario) {
      this.comentarioValue = comentario.texto;
    }
    // console.log(this.comentarioValue)
    // this.comentarioService.UpdateString(text).subscribe((topicos) => (this.text = topicos));
  }
}
