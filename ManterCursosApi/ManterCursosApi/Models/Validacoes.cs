namespace ManterCursosApi.Models
{
    public class Validacoes
    {
        public static string ErrorCursoPeriodo { get; } = "Existe(m) curso(s) planejados(s) dentro do período informado";
        public static string ErrorCursoJaCadastrado { get; } = "Já existe um curso com este nome cadastrado";
        public static string ErrorDataFinalMenorInicial { get; } = "Data final não pode ser menor que a data inicial";
        public static string ErrorDataInicialMenorAtual { get; } = "Data inicial do curso não pode ser menor do que hoje";
        public static string ErrorNaoEncontrado { get; } = "Nao Encontrado";
        public static string ErrorExclusaoCursoConcluido{ get; } = "Não é permitida a exclusão de um curso concluido";
        public static string CursoOK { get; } = "Ok";
    }
}
