import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForumComponent } from './components/forum/forum.component';
import { LoginComponent } from './components/forum/usuario/login/login.component';
import { RegistrarUsuarioComponent } from './components/forum/usuario/registrar-usuario/registrar-usuario.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: ForumComponent },
  { path: 'login', component: LoginComponent },
  { path: 'cadastrar', component: RegistrarUsuarioComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
