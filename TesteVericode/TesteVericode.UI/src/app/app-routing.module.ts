import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TarefaAddComponent } from './components/tarefa-add/tarefa-add.component';
import { TarefaDetailsComponent } from './components/tarefa-details/tarefa-details.component';
import { TarefasListComponent } from './components/tarefas-list/tarefas-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'tarefas', pathMatch: 'full' },
  { path: 'tarefas', component: TarefasListComponent },
  { path: 'tarefas/:id', component: TarefaDetailsComponent },
  { path: 'add', component: TarefaAddComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
