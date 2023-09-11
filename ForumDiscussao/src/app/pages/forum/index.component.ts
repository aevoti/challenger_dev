import { Component,OnInit } from '@angular/core';
import {forumModule} from './forum.modules';
import {ForumService} from './forum.service';
import {formatDate} from '@angular/common';
import { topicos,ordenacao } from './models';
import { ComentarioComponent } from './comentario/comentario.component';




@Component({
  selector: 'app-root',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class ForumComponent implements OnInit{
  public objTeste = [1,2,3]
  public lstOrdenacao: ordenacao[]= [
    {name:'Mais Recentes',code:0},
    {name:'Mais Antigos',code:1}
  ];

  public idTopicSel :number = 0;
  public textUpdate : string='';
  public textPesquisa : string='';

  
  public ordenacaoSelecionada : ordenacao={name:'Mais Recentes',code:0};

  visible: boolean = false;

  showDialog(dsc:string,idTopico:number) {
    this.textUpdate = dsc;
    this.idTopicSel=idTopico;
    this.visible = true;
  }
  public newTopic : string='';
  public topics : topicos[] =[] ;
  selectedCities: any = [];

  ngOnInit() {
    this.getTopico();
  }
  constructor(
    private forumServ: ForumService
  ){}
  
  postTopico(){
    if(!this.newTopic || this.newTopic=="") return
    this.forumServ.postTopico(this.newTopic).subscribe(r=>{console.log(r); this.getTopico();});
  }

  updateTopico(){
    if(!this.textUpdate || this.textUpdate=="") return
    this.visible=false;
    this.forumServ.updateTopico(this.idTopicSel,this.textUpdate).subscribe(r=>{console.log(r); this.getTopico();});
    
  }

  deleteTopico(idTopic : number){
    this.visible=false;
    this.forumServ.deleteTopico(idTopic).subscribe(r=>{console.log(r); this.getTopico();});
  }

  getTopico(){
    this.topics=[]
    this.forumServ.getTopicos(this.ordenacaoSelecionada.code).subscribe(r=>{ 
      r.forEach((element) => {element.data = formatDate(element.data,'dd-MM-yyyy','pt-br');});
      this.topics = r
    });    
  }

  pesquisarTopico(){

    if (!this.textPesquisa || this.textPesquisa.trim()==""){
      this.getTopico();
    }else{
      this.forumServ.pesquisarTopico(this.textPesquisa,this.ordenacaoSelecionada.code).subscribe(r=>{ 
        r.forEach((element) => {element.data = formatDate(element.data,'dd-MM-yyyy','pt-br');});
        this.topics = r
      });   
    }

     
  }

    
}


