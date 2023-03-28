import { Component, OnInit } from '@angular/core';
import { Tarefa } from 'src/app/models/tarefa';
import { TarefaService } from 'src/app/services/tarefa.service';

@Component({
  selector: 'app-tarefas-list',
  templateUrl: './tarefas-list.component.html',
  styleUrls: ['./tarefas-list.component.css']
})
export class TarefasListComponent implements OnInit {

  tarefas?: Tarefa[];
  currentTarefa: Tarefa = {};
  currentIndex = -1;
  title = '';

  constructor(private tarefaService: TarefaService) { }

  ngOnInit(): void {
    this.retrieveTarefas();
  }

  retrieveTarefas(): void {
    this.tarefaService.getAll()
      .subscribe({
        next: (data) => {
          console.log(data)
          this.tarefas = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveTarefas();
    this.currentTarefa = {};
    this.currentIndex = -1;
  }

  setActiveTarefa(tarefa: Tarefa, index: number): void {
    this.currentTarefa = tarefa;
    this.currentIndex = index;
  }  
  
}
