import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TarefasListComponent } from './components/tarefas-list/tarefas-list.component';
import { TarefaDetailsComponent } from './components/tarefa-details/tarefa-details.component';
import { TarefaAddComponent } from './components/tarefa-add/tarefa-add.component';

@NgModule({
  declarations: [
    AppComponent,
    TarefasListComponent,
    TarefaDetailsComponent,
    TarefaAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
