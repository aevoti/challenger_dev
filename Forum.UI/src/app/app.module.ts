import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ShellModule } from './components/shell/shell.module';
import { ForumComponent } from './components/forum/forum.component';
import { ForumModule } from "./components/forum/forum.module";
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [
        AppComponent,
        ForumComponent,
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        CommonModule,
        AppRoutingModule,
        HttpClientModule,
        NgbModule,
        ShellModule,
        ForumModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot({
          preventDuplicates: true,
        }),
        JwtModule.forRoot({
            config: {
              tokenGetter: () => {
                return localStorage.getItem('token');
              },
              allowedDomains: [],
              disallowedRoutes: [],
            },
          }),
    ]
})
export class AppModule { }