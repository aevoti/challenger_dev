import { Component } from '@angular/core';
import { TopicoService } from './services/topicoService/topico.service';
import { OrderService } from './services/orderService/order.service';
import { Topico } from './models/topico';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ForumApp';
  order = "Decrescente"
  searchText = ""
  topicos:Topico[] = []

  constructor(private topicoService:TopicoService, private orderService: OrderService, private toastr: ToastrService) { }

  pesquisar(): void {
    this.topicoService.getTopicos(this.order, this.searchText).subscribe(
      {
        next: (topicos) => {
          this.topicos = topicos
          this.showSuccess("Pesquisa realizada")
        },
        error: (err) => {
          this.showError(err.error.title);
        }
      }
    );
  }

  setOrder(newOrder: string): void {
    this.order = newOrder;
    this.pesquisar();
  }

  setSearchText(searchText: string): void {
    this.searchText = searchText;
    this.pesquisar();
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Sucesso');
  }

  showError(message: string) {
    this.toastr.error(message, 'Erro');
  }
}
