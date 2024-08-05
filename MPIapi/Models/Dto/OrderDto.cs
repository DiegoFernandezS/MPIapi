using System.ComponentModel.DataAnnotations;

namespace MPIapi.Models.Dto
{
    using System.ComponentModel.DataAnnotations;

public class OrderDto
{
    // Crear todas las propiedades según el JSON recibido
    public int Id { get; set; }

    [Required]
    [MaxLength(20)] // DataAnnotations ... (Es requerido, máximo de caracteres, etc)
    public string Name { get; set; } = string.Empty; // Inicializar con una cadena vacía

    [Required]
    public int cantProductos { get; set; }
}
}