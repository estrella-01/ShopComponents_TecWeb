namespace ShopComponents.Core.QueryFilters;

public class MantenimientoFilter
{
    public int? ClienteId { get; set; }
    public string? Estado { get; set; }
    public DateOnly? FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
    public string? Descripcion { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
