import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tarefa } from 'src/app/models/tarefa';
import { TarefaService } from 'src/app/services/tarefa.service';

@Component({
  selector: 'app-tarefa-details',
  templateUrl: './tarefa-details.component.html',
  styleUrls: ['./tarefa-details.component.css']
})
export class TarefaDetailsComponent implements OnInit {

  @Input() viewMode = false;

  @Input() currentTarefa: Tarefa = {
    descricao: '',
    status: ''
  };
  
  message = '';

  constructor(
    private tarefaService: TarefaService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getTarefa(this.route.snapshot.params["id"]);
    }
  }

  getTarefa(id: string): void {
    this.tarefaService.getbyId(id)
      .subscribe({
        next: (data) => {
          this.currentTarefa = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }  

  updateTarefa(): void {
    this.message = '';

    this.tarefaService.update(this.currentTarefa.id, this.currentTarefa)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.message = res.message ? res.message : 'Tarefa atualiza com sucesso!';
        },
        error: (e) => console.error(e)
      });
  }

  deleteTarefa(): void {
    this.tarefaService.delete(this.currentTarefa.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['/tarefas']);
        },
        error: (e) => console.error(e)
      });
  }

}
