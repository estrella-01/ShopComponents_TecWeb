using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Detalleventum
{
    public int Id { get; set; }

    public int VentaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Ventum Venta { get; set; } = null!;
}
