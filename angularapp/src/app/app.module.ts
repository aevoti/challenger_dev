import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatListModule} from '@angular/material/list';
import { AppComponent } from './app.component';
import { TopicsComponent } from './topics/topics/topics.component';
import { CreateTopicModalComponent } from './create-topic-modal/create-topic-modal.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { EditTopicModalComponent } from './edit-topic-modal/edit-topic-modal.component';
import { CreateCommentModalComponent } from './create-comment-modal/create-comment-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    TopicsComponent,
    CreateTopicModalComponent,
    EditTopicModalComponent,
    CreateCommentModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatListModule,
    FormsModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
