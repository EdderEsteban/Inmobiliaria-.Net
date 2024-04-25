using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net.Models
{
      public class Contrato
    {
        [Key]
        public int Id_contrato { get; set; }
        public Inquilino? Inquilino { get; set; }
        public Inmueble? Inmueble { get; set; }

        [Required(ErrorMessage = "El Id del inquilino es obligatorio.")]
        public int Id_inquilino { get; set; }

        [Required(ErrorMessage = "El Id del inmueble es obligatorio.")]
        public int Id_inmueble { get; set; }

        public int Monto { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Fecha_inicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [Display(Name = "Fecha de Fin")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Fecha_fin { get; set; }
        public int Id_usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now; 
        
    }
}

