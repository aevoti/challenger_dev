import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TopicoService } from 'src/app/services/topicoService/topico.service';

@Component({
  selector: 'app-createTopic',
  templateUrl: './createTopic.component.html',
  styleUrls: ['./createTopic.component.scss']
})
export class CreateTopicComponent implements OnInit {

  form!:FormGroup;

  constructor(private fb:FormBuilder, private topicoService:TopicoService) { }

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
    this.topicoService.createTopico(novoTopico).subscribe(topico => {console.log(topico)})
  }
}
