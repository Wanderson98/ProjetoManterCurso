import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Categoria } from 'src/app/model/Categoria';
import { Curso } from 'src/app/model/Curso';
import { CategoriasService } from 'src/app/services/categorias.service';
import { CursosService } from 'src/app/services/cursos.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-listar-cursos',
  templateUrl: './listar-cursos.component.html',
  styleUrls: ['./listar-cursos.component.css'],
})

export class ListarCursosComponent implements OnInit {
  constructor(
    private cursoService: CursosService,
    private categoriaService: CategoriasService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) {}

  cursosAtivos!: Curso[];
  cursosFiltradosDescricao!: Curso[];
  categorias!: Categoria[];
  cursoForm!: FormGroup;
  buscaForm!: FormGroup
  tituloModal!: string;
  idExcluir!: number;
  dataAtual: any = new Date();
  dataInicialFiltro: any;
  dataFinalFiltro: any;
  private _filtroCursosDescricao!: string;

  ngOnInit(): void {
    
    this.CarregarCursosAtivos();
    this.CarregarCategorias();
    this.tituloModal = 'Adicionar curso';
    this.cursoForm = this.formBuilder.group({
      cursoId: [0],
      cursoDescricao: ['', Validators.compose([Validators.required])],
      cursoDataInicial: ['', Validators.compose([Validators.required])],
      cursoDataFinal: ['', Validators.compose([Validators.required])],
      cursoQuantidadeAlunos: [0],
      isAtivo: [true],
      categoriaId: ['', Validators.required],
    });

    this.buscaForm = this.formBuilder.group({
      dataInicialFiltro : [],
      dataFinalFiltro : [],
      busca: []
    })
  }

  public get filtroListaDescricao() {
    return this._filtroCursosDescricao;
  }

  public set filtroListaDescricao(value: string) {
    this._filtroCursosDescricao = value;
    this.cursosFiltradosDescricao = this.filtroListaDescricao
      ? this.filtrarCursosDescricao(this.filtroListaDescricao)
      : this.cursosAtivos;
  }

  filtrarCursosDescricao(filtro: string): any {
    filtro = filtro.toLocaleLowerCase();
    return this.cursosAtivos.filter(
      (cursos: { cursoDescricao: string }) =>
        cursos.cursoDescricao.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  }

  CarregarCursosAtivos(): void {
    this.cursoService.ListarTodosCursosAtivos().subscribe((data) => {
      this.cursosAtivos = data;
      this.cursosFiltradosDescricao = data;
    });
  }

  LimparFormulario(): void {
    this.cursoForm.reset();
    this.cursoForm.controls['cursoId'].setValue(0);
    this.cursoForm.controls['isAtivo'].setValue(true);
    this.cursoForm.controls['cursoQuantidadeAlunos'].setValue(0);
    this.idExcluir = 0;
  }

  CarregarCategorias(): void {
    this.categoriaService.ListarTodasCategorias().subscribe((data) => {
      this.categorias = data;
    });
  }

  CarregarModalEditar(item: any) {
    this.tituloModal = 'Editar Curso';
    this.cursoForm.controls['cursoId'].setValue(item.cursoId);
    this.cursoForm.controls['cursoDescricao'].setValue(item.cursoDescricao);
    this.cursoForm.controls['cursoDataInicial'].setValue(item.cursoDataInicial);
    this.cursoForm.controls['cursoDataFinal'].setValue(item.cursoDataFinal);
    this.cursoForm.controls['isAtivo'].setValue(item.isAtivo);
    this.cursoForm.controls['cursoQuantidadeAlunos'].setValue(
      item.cursoQuantidadeAlunos
    );
    this.cursoForm.controls['categoriaId'].setValue(item.categoriaId);
  }

  EnviarFormulario() {
    const curso: Curso = this.cursoForm.value;
    console.log(curso)
    if(this.cursoForm.valid){

    if (curso.cursoId < 1) {
      this.cursoService.AdicionarCurso(curso).subscribe({
        next: (res) => {
          
          this.toastr.success('Curso Inserido com Sucesso!', 'Gravando!');
          this.LimparFormulario();
          this.CarregarCursosAtivos();
        },
        error: (res) => {
          console.log(res);
          if (res.error.errors || res.error == null) {
            this.toastr.error('Erro Inesperado', 'Error');
          } else {
            this.toastr.error(res.error, 'Error');
          }
        },
      });
    } else {
      this.cursoService.AtualizarCurso(curso).subscribe({
        next: (res) => {
          this.toastr.success('Curso Atualizado com Sucesso!', 'Atualizando!');
          this.LimparFormulario();
          this.CarregarCursosAtivos();
        },
        error: (res) => {
          if (res.error.errors || res.error == null) {
            this.toastr.error('Erro Inesperado', 'Error');
          } else {
            this.toastr.error(res.error, 'Error');
          }
        },
      });
    }
    } else {
      this.toastr.error('Preencha todos os campos obrigatórios', 'Error');
    }
  }

  ExcluirCurso(cursoId: number) {
    this.cursoService.ExcluirCurso(cursoId).subscribe({
      next: (res) => {
        this.toastr.warning('Curso Excluido com Sucesso!', 'Excluindo!');
        this.LimparFormulario();
        this.CarregarCursosAtivos();
      },
      error: (res) => {
        if (res.error.errors || res.error == null) {
          this.toastr.error('Erro Inesperado', 'Error');
        } else {
          this.toastr.error(res.error, 'Error');
        }
      },
    });
  }

  PegarIdExclusao(cursoId: number) {
    this.idExcluir = cursoId;
  }

  filtroData(){

    if(this.dataInicialFiltro > this.dataFinalFiltro && this.dataFinalFiltro ){
      this.toastr.error('Data final não pode ser menor que a data inicial');
    } else if(!this.dataInicialFiltro && !this.dataFinalFiltro){
      this.CarregarCursosAtivos();
    }   
    else if (this.dataInicialFiltro && !this.dataFinalFiltro){
      this.filtrarCursoDataInicial(this.dataInicialFiltro);
    }else if (!this.dataInicialFiltro && this.dataFinalFiltro){
      this.filtrarCursoDataFinal(this.dataFinalFiltro);
    }else {
      this.filtrarCursoDataInicialFinal(this.dataInicialFiltro, this.dataFinalFiltro)
    }
  }

  filtrarCursoDataInicial(dataInicial: any) : any
  {
    this.cursosFiltradosDescricao = this.cursosAtivos.filter(result =>{
      return result.cursoDataInicial >= dataInicial || result.cursoDataFinal >= dataInicial
    })
  }

  filtrarCursoDataFinal(dataFinal: any) : any
  {
    this.cursosFiltradosDescricao = this.cursosAtivos.filter(result =>{
      return result.cursoDataInicial <= dataFinal || result.cursoDataFinal <= dataFinal
    })
  }

  filtrarCursoDataInicialFinal(dataInicial:any, dataFinal: any) : any
  {
    this.cursosFiltradosDescricao = this.cursosAtivos.filter(result =>{
      return (result.cursoDataInicial >= dataInicial || result.cursoDataFinal >= dataInicial) && (result.cursoDataInicial <= dataFinal || result.cursoDataFinal <= dataFinal)
    })
  }

  LimparFiltro(){
    this.buscaForm.reset();
    this.CarregarCursosAtivos();
  }
}
