using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Inventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string? TipoMovimiento { get; set; }

    public int Cantidad { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
