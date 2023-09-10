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
import { TopicoComponent } from 'src/components/topico/topico.component';
import { ComentarioComponent } from 'src/components/comentario/comentario.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CreateTopicComponent,
    SetOrderComponent,
    TopicoComponent,
    ComentarioComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({positionClass: 'toast-bottom-right'})
  ],
  providers: [
    TopicoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
