using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Proforma
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Detalleproforma> Detalleproformas { get; set; } = new List<Detalleproforma>();
}
