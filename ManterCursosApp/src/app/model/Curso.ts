import { Categoria } from "./Categoria";

export class Curso{

    cursoId: number = 0;
    cursoDescricao!: string;
    cursoDataInicial!: Date;
    cursoDataFinal!: Date;
    cursoQuantidadeAlunos: number = 0;
    isAtivo: boolean = true;
    categoriaId!: number;
    categoria!: Categoria;

}