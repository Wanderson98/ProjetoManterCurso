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

  ngOnInit(): void {
    this.CarregarLogs();
  }

  CarregarLogs(){
    this.logService.pegarLogs().subscribe(data =>{
     this.logs = data

    })
  }
}
