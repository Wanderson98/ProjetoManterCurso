import { Categoria } from "./Categoria";

export class Curso{

    cursoId!: number;
    cursoDescricao!: string;
    cursoDataInicial!: Date;
    cursoDataFinal!: Date;
    cursoQuantidadeAlunos!: number;
    isAtivo!: boolean;
    categoriaId!: number;
    categoria!: Categoria;

}