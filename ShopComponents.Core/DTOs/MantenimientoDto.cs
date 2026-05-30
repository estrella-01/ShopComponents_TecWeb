namespace ShopComponents.Core.DTOs;

public class MantenimientoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Fecha { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public string? Observaciones { get; set; }
}
