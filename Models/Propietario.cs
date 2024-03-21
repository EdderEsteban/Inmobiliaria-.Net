using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net.Models;

public class Propietario
{
    [Key]
    public int? Id_inquilino { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Apellido { get; set; }
    [Required]
    public string? Dni { get; set; }
    [Required]
    public string? Direccion { get; set; }
    [Required]
    public string? Telefono { get; set; }
    [Required]
    public string? Email { get; set; }    
}