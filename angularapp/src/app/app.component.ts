import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { TopicsComponent } from './topics/topics/topics.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(http: HttpClient) {

  }

  title = 'angularapp';
}
