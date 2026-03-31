using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Detalleproforma> Detalleproformas { get; set; } = new List<Detalleproforma>();

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
