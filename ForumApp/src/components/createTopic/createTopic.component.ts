import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TopicoService } from 'src/app/services/topicoService/topico.service';

@Component({
  selector: 'app-createTopic',
  templateUrl: './createTopic.component.html',
  styleUrls: ['./createTopic.component.scss']
})
export class CreateTopicComponent implements OnInit {

  form!:FormGroup;

  constructor(private fb:FormBuilder, private topicoService:TopicoService, private toastr: ToastrService) { }

  ngOnInit() {
    this.buildForm();
  }

  buildForm(){
    this.form = this.fb.group({
      titulo:this.fb.control(null, Validators.required),
      descricao:this.fb.control(null, Validators.required)
    });
  }

  submit(event:SubmitEvent){
    event.preventDefault()
    const novoTopico = {
      ...this.form.value,
      usuarioId: 1
    }
    this.form.reset()
    this.topicoService.createTopico(novoTopico).subscribe(
      {
        next: () => {
          this.showSuccess("Tópico criado")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
      )
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Sucesso');
  }

  showError(message: string) {
    this.toastr.error(message, 'Erro');
  }
}
