import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  order: string = 'Decrescente';

  constructor() {}

  setOrder(newOrder: string): void {
    this.order = newOrder;
  }

  getOrder(): string {
    return this.order;
  }
}