using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net.Models;

public class Inquilino
{
    
    public int Id_inquilino { get; set; }
    
    public string? Nombre { get; set; }
    
    public string? Apellido { get; set; }
    
    public int? Dni { get; set; }
    
    public string? Direccion { get; set; }
   
    public string? Telefono { get; set; }
   
    public string? Correo { get; set; }    
}