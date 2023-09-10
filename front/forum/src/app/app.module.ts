import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CriarTopicoComponent } from './components/topicos/criar-topico/criar-topico.component';
import { ListarTopicoComponent } from './components/topicos/listar-topico/listar-topico.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CriarTopicoComponent,
    ListarTopicoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
