import { Component , inject} from '@angular/core';
import { Topico } from 'src/app/Topico';

import { TopicoService } from 'src/app/service/topico.service';
import { SharedServiceService } from '../../service/shared-service.service';

@Component({
  selector: 'app-topicos',
  templateUrl: './topicos.component.html',
  styleUrls: ['./topicos.component.scss']
})
export class TopicosComponent  {

  topicos : Topico[] = [];
  selectedValue: string = '';
  filtro: string = '';

  constructor(private topicoService : TopicoService,
    private sharedDataService: SharedServiceService) {
    
  }

  ngOnInit() {
    this.sharedDataService.selectedValue$.subscribe((value) => {
      this.selectedValue = value;
      // Faça o que quiser com o valor selecionado aqui
      this.getTopicosData()
    });
  }

  getTopicos(): void{
    this.topicoService.getTodosTopicos().subscribe((topicos) =>(this.topicos = topicos));
  }

  getTopicosData(): void{
    const selectedOption = this.selectedValue;
    this.topicoService.getTopicoPorData(this.selectedValue).subscribe((topicos) =>(this.topicos = topicos));
    
  }
  getTopicosDataFiltro(): void{
    const selectedOption = this.selectedValue;
    const filtro = this.selectedValue;
    this.topicoService.getTopicoPorDataFiltro(this.selectedValue,filtro).subscribe((topicos) =>(this.topicos = topicos));
    
  }
} 

