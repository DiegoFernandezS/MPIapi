namespace MPIapi.Models
{
    public class Order
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Inicialización de la propiedad

    public int CantProductos { get; set; }

    public Order()
    {
        // También se puede inicializar en el constructor, aunque no es necesario si ya se ha inicializado como arriba
        // Name = string.Empty;
    }
}
}
