import { Component } from '@angular/core';
import { Tarefa } from 'src/app/models/tarefa';
import { TarefaService } from 'src/app/services/tarefa.service';

@Component({
  selector: 'app-tarefa-add',
  templateUrl: './tarefa-add.component.html',
  styleUrls: ['./tarefa-add.component.css']
})
export class TarefaAddComponent {
  tarefa: Tarefa = {
    descricao: '',
    status: ''
  };
  submitted = false;

  constructor(private tarefaService: TarefaService) { }

  saveTarefa(): void {
    const data = {
      descricao: this.tarefa.descricao,
      status: this.tarefa.status,
      data: this.tarefa.data
    };

    this.tarefaService.create(data)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.submitted = true;
        },
        error: (e) => console.error(e)
      });
  }

  newTarefa(): void {
    this.submitted = false;
    this.tarefa = {
      descricao: '',
      status: ''
    };
  }
}
