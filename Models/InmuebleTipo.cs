using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net.Models
{
    public class InmuebleTipo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo {1} caracteres.")]
        public string? Tipo { get; set; }

        
    }
}
