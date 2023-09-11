import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule } from 'primeng/dropdown';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {ForumService} from './forum.service'
import { LOCALE_ID } from '@angular/core';



import { ForumComponent } from './index.component';
import { ComentarioComponent } from './comentario/comentario.component';

import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    ForumComponent,ComentarioComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    MultiSelectModule,
    BrowserAnimationsModule,
    InputTextModule,
    DropdownModule,
    HttpClientModule,
    ButtonModule,
    DialogModule,
    
    
  ],
  exports: [
    ForumComponent,ComentarioComponent
  ],
  providers: [
    ForumService,HttpClient,HttpClientModule,{provide: LOCALE_ID, useValue: 'pt-br'}
  ]
})

export class forumModule { }
