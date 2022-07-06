using System.ComponentModel.DataAnnotations;

namespace AndradeCursosApi.Models
{
    
    public class Log
    {
        public int LogId { get; set; }
        [Required]
        public int CursoId { get; set; }            
        public Curso Curso { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime LogDataInclusao { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LogDataAtualizacao { get; set; }
    }
}

