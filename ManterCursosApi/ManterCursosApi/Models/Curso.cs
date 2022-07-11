using System.ComponentModel.DataAnnotations;

namespace ManterCursosApi.Models;

public class Curso
{

    public int CursoId { get; set; }
    [Required]
    [StringLength(500)]
    public string CursoDescricao { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime CursoDataInicial { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime CursoDataFinal { get; set; }
    public int? CursoQuantidadeAlunos { get; set; }
    public bool IsAtivo { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}

