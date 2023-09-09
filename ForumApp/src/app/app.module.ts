import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from 'src/components/header/header.component';
import { CreateTopicComponent } from 'src/components/createTopic/createTopic.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TopicoService } from './services/topicoService/topico.service';
import { HttpClientModule } from '@angular/common/http';
import { SetOrderComponent } from 'src/components/setOrder/setOrder.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CreateTopicComponent,
    SetOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    TopicoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
