using System.ComponentModel.DataAnnotations;

namespace ManterCursosApi.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoriaNome { get; set; }
    }
}
