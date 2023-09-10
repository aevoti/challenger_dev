import { Component, EventEmitter, OnInit, Output } from '@angular/core';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  
  searchText = ""

  @Output() onSearch = new EventEmitter()
  constructor() { }

  ngOnInit() {
  }

  handleSearch(): void {
    this.onSearch.emit(this.searchText)
  }
}
