using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Ventum
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal Total { get; set; }

    public int? ClienteId { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Usuario? Usuario { get; set; }
}
