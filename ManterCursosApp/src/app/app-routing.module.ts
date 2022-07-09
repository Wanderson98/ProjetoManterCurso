import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarCursosComponent } from './componentes/cursos/listar-cursos/listar-cursos.component';
import { HomeComponent } from './componentes/home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'cursos', component: ListarCursosComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
