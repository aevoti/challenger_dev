import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { OrderService } from 'src/app/services/orderService/order.service';

@Component({
  selector: 'app-setOrder',
  templateUrl: './setOrder.component.html',
  styleUrls: ['./setOrder.component.scss']
})
export class SetOrderComponent implements OnInit {

  @Output() onChange = new EventEmitter()
  constructor(private orderService: OrderService) {}

  ngOnInit() {
  }

  onOrderChange(event: any): void {
    this.onChange.emit(event.target.value)
  }
}
