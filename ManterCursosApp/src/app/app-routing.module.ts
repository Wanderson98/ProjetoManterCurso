import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarCursosComponent } from './componentes/cursos/listar-cursos/listar-cursos.component';
import { HomeComponent } from './componentes/home/home.component';
import { ListarLogsComponent } from './componentes/listar-logs/listar-logs.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'cursos', component: ListarCursosComponent},
  {path: 'logs', component: ListarLogsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
