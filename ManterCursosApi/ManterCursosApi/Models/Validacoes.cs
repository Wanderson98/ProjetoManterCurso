namespace ManterCursosApi.Models
{
    public class Validacoes
    {
        public static string ErrorCursoPeriodo { get; } = "Cod-001 Existe(m) curso(s) planejados(s) dentro do período informado";
        public static string ErrorCursoJaCadastrado { get; } = "Cod-002 Já existe um curso com este nome/descrição cadastrado";
        public static string ErrorDataFinalMenorInicial { get; } = "Cod-003 Data final não pode ser menor que a data inicial";
        public static string ErrorDataInicialMenorAtual { get; } = "Cod-004 Data inicial do curso não pode ser menor do que a atual";
        public static string ErrorNaoEncontrado { get; } = "Cod-005 Nao Encontrado";
        public static string ErrorExclusaoCursoConcluido { get; } = "Cod-006 Não é permitida a exclusão de um curso concluido";
        public static string CursoOK { get; } = "Ok";
    }
}
