import { Component, OnInit,Input } from '@angular/core';
import {ForumService} from '../forum.service';
import { comentario } from '../models';
import {formatDate} from '@angular/common';

import {comentarioModule} from './comentario.modules'


@Component({
  selector: 'comentario',
  templateUrl: './comentario.component.html',
  styleUrls: ['./comentario.component.scss']
})
export class ComentarioComponent {
  @Input() idTopico:number = 0;
  public visible:boolean=false;
  public dsc : string ='';
  public textUpdate : string='';
  public idUpdate : number=0;
  public coments : comentario[]=[];
  constructor(
    private forumServ: ForumService
  ){}
  
  ngOnInit(){

  }
  postComent(){
    if(!this.dsc || this.dsc=="") return
    this.forumServ.postComent(this.idTopico,this.dsc).subscribe(r=>{console.log(r); this.getComent();});
  }
  getComent(){
    this.forumServ.getComent(this.idTopico).subscribe(r=>{
      r.forEach((element) => {element.data = formatDate(element.data,'dd-MM-yyyy','pt-br');});
      this.coments = r
      this.limpaVar();
    });
  }

  deleteComent(id :number){
    this.forumServ.deleteComent(this.idTopico,id).subscribe(r=>{console.log(r); this.getComent();});
  }

  updateComent(id :number){
    if(!this.textUpdate || this.textUpdate=="") {
      this.getComent();
      return
    }
    this.forumServ.updateComent(this.idTopico,this.idUpdate,this.textUpdate).subscribe(r=>{console.log(r); this.getComent();});
  }

  
  showDialog(dsc:string,id:number) {
    this.textUpdate = dsc;
    this.idUpdate= id;
    this.visible = true;
  }

  limpaVar(){
     this.visible=false;
     this.dsc ='';
     this.textUpdate='';
     this.idUpdate=0;
  }

}
