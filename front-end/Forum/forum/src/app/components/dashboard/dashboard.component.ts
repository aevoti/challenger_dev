import { Component } from '@angular/core';
import { SharedServiceService } from '../../service/shared-service.service';

import { TopicoService } from 'src/app/service/topico.service';

import { Topico } from 'src/app/Topico';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  selectedValue: string = 'decrescente';
  filter: string = '';
  topicos : Topico[] = [];
  text :string = '';
  searchForm = this.fb.nonNullable.group({
    filter :'',
  });
  
  constructor(private sharedServiceService: SharedServiceService,
     private topicoService: TopicoService,
     private fb: FormBuilder,
     private http:HttpClient) {
    
    this.getTopicos()
  }
  
  ngOnInit(){
    this.onComboBoxChange();
  }
  
  textareaValue = ''; // Variável para armazenar o valor da textarea
  searchValue = '';

  // Função para verificar se a textarea está vazia
  isTextareaEmpty() {
    return this.textareaValue.trim() === '';
  }

  isTextFilterEmpty() {
    return this.searchValue.trim() === '';
  }

 
  onTopicoCreate(text: string) {
    console.log(text); // Exibe a string no console, se necessário
    this.topicoService.createString(text).subscribe((topicos) => (this.text = topicos));
  }


  getTopicos(): void{
    this.topicoService.getTodosTopicos().subscribe((topicos) =>(this.topicos = topicos));
  }

  
  onSearchSubmit(filter: {text : string}){
    console.log(filter);
  }

  onComboBoxChange() {
    const selectedOption = this.selectedValue;
    this.sharedServiceService.setSelectedValue(selectedOption); // Use o serviço para definir o valor selecionado
    this.topicoService.getTopicoPorData(selectedOption).subscribe((topicos) => {
      this.topicos = topicos;
    });
  }

  search(){
    console.log("entrou aqui");
  }
}
