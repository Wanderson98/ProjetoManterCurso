import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Logs } from 'src/app/model/Logs';
import { LogsService } from 'src/app/services/logs.service';

@Component({
  selector: 'app-listar-logs',
  templateUrl: './listar-logs.component.html',
  styleUrls: ['./listar-logs.component.css']
})
export class ListarLogsComponent implements OnInit {

  constructor(private logService : LogsService,  private toastr: ToastrService) { }

  logs!: Logs[];
  logsFiltrados!: Logs[];
  _filtroLog!: string;

  ngOnInit(): void {
    this.CarregarLogs();
  }

  CarregarLogs(){
    this.logService.pegarLogs().subscribe(data =>{
     this.logs = data;
     this.logsFiltrados = data;

    })
  }

  public get filtroLog() {
    return this._filtroLog;
  }

  public set filtroLog(value: string) {
    this._filtroLog = value;
    this.logsFiltrados = this.filtroLog
      ? this.filtrarLogs(this.filtroLog)
      : this.logs;
  }

  filtrarLogs(filtro: string): any {
    filtro = filtro.toLocaleLowerCase();
    return this.logs.filter(
      (logs: Logs) =>
        logs.curso.cursoDescricao.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  } 
}
