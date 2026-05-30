namespace ShopComponents.Core.Entities;

public partial class Mantenimiento
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateOnly Fecha { get; set; }
    public string Descripcion { get; set; } = null!;
    public decimal Costo { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public string? Observaciones { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
