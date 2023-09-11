import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit{
  token: string | null = null;

  constructor() { }

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
    window.location.reload();
    this.token = null;
  }
}
