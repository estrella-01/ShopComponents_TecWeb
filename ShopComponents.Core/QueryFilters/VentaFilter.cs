using System;
using System.Collections.Generic;
using System.Text;

namespace ShopComponents.Core.QueryFilters;

public class VentaFilter
{
    public int? ClienteId { get; set; }
    public int? UsuarioId { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public string? Texto { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}