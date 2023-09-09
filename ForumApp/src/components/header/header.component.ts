import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from 'src/app/services/orderService/order.service';
import { TopicoService } from 'src/app/services/topicoService/topico.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  searchText: string = '';

  constructor(private topicoService:TopicoService, private orderService: OrderService) { }

  ngOnInit() {
    this.pesquisar();
  }

  pesquisar(): void {
    const order = this.orderService.getOrder();
    this.topicoService.getTopicos(order, this.searchText).subscribe(topicos => {
      console.log(topicos, order, this.searchText)
    });
  }

}
