using System;
using System.Collections.Generic;
using System.Text;

namespace ShopComponents.Core.DTOs;

public class FacturaDto
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public string? NroFactura { get; set; }
    public DateTime? Fecha { get; set; }
    public decimal? Total { get; set; }
}
