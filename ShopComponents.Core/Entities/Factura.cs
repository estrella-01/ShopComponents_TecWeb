using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Factura
{
    public int Id { get; set; }

    public int VentaId { get; set; }

    public string? NroFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public virtual Ventum Venta { get; set; } = null!;
}
