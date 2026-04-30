namespace ShopComponents.Core.DTOs;

public class InventarioDto
{
    public int ProductoId { get; set; }
    public string TipoMovimiento { get; set; }  // "entrada" o "salida"
    public int Cantidad { get; set; }
}