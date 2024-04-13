using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net.Models
{
    public class Inmueble
    {
        [Key]
        public int Id_inmueble { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo Dirección debe tener como máximo {1} caracteres.")]
        public string? Direccion { get; set; }

        public UsoInmueble? Uso { get; set; }

        public int Id_tipo { get; set; }

        public InmuebleTipo? Tipo { get; set; }

        [Required(ErrorMessage = "El campo Cantidad de Ambientes es obligatorio.")]
        public int Cantidad_Ambientes { get; set; }

        [Required(ErrorMessage = "El campo Precio de Alquiler es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio de Alquiler debe ser mayor o igual a 0.")]
        public decimal Precio_Alquiler { get; set; }

        [Required(ErrorMessage = "El campo Latitud es obligatorio.")]
        [RegularExpression("^[0-9.-]*$", ErrorMessage = "El campo Latitud solo debe contener números y el caracter '.'.")]
        public string? Latitud { get; set; } 

        [Required(ErrorMessage = "El campo Longitud es obligatorio.")]
        [RegularExpression("^[0-9.-]*$", ErrorMessage = "El campo Latitud solo debe contener números y el caracter '.'.")]
        public string? Longitud { get; set; }

        
        public Boolean Activo { get; set; }

        public Boolean Disponible { get; set; }

        public int Id_propietario { get; set; }

        public Propietario? Propietario { get; set; }


        public int Id_usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now; 

    }

    public enum UsoInmueble     
    
    {
        Comercial = 1,
        Residencial = 2
    }
}

