namespace ShopComponents.Core.DTOs;

public class ProformaDto
{
    public int Id { get; set; }
    public int? ClienteId { get; set; }
    public DateTime? Fecha { get; set; }
    public decimal? Total { get; set; }
    public List<DetalleProformaDto> Detalles { get; set; } = new();
}

public class DetalleProformaDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}