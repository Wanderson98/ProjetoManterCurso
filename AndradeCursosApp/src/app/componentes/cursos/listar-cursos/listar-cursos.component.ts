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
  styleUrls: ['./listar-cursos.component.css']
})
export class ListarCursosComponent implements OnInit {

  constructor(private cursoService: CursosService, private categoriaService : CategoriasService, 
    private formBuilder: FormBuilder, private toastr: ToastrService
    ) { }

  cursosAtivos!: Curso[];
  categorias!: Categoria[];
  cursoForm!: FormGroup;

  ngOnInit(): void {
    this.carregarCursosAtivos();
    this.carregarCategorias();

    this.cursoForm = this.formBuilder.group({
      cursoId : [0],
      cursoDescricao: ['', Validators.compose([Validators.required])],
      cursoDataInicial: ['', Validators.compose([Validators.required])],
      cursoDataFinal: ['', Validators.compose([Validators.required])],
      cursoQuantidadeAlunos: [0],
      isAtivo: [true],
      categoriaId: ['', Validators.required]
    });
  }

  carregarCursosAtivos(): void
  {
    this.cursoService.ListarTodosCursosAtivos().subscribe( data =>{
      this.cursosAtivos = data;
     
    })
  }

  carregarCategorias():void
  {
    this.categoriaService.ListarTodasCategorias().subscribe(data =>{
      this.categorias = data;
      console.log(this.categorias)
    })
  }

  EnviarFormulario(){
    const curso : Curso = this.cursoForm.value;
    console.log(curso)
    
    this.cursoService.AdicionarCurso(curso).subscribe(
      {
        next:(res) => {
         
          this.toastr.success('Curso Inserido com Sucesso!', 'Gravando!');
          this.cursoForm.reset();
          this.carregarCursosAtivos();


        },
        error:(res)=> {
          console.log(res)
          this.toastr.error(res.error, 'Error')
        }
      } 
        
    );
  }
}
