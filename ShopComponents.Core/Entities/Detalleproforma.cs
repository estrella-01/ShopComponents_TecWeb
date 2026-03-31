using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Detalleproforma
{
    public int Id { get; set; }

    public int? ProformaId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Proforma? Proforma { get; set; }
}
