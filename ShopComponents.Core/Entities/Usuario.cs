using System;
using System.Collections.Generic;

namespace ShopComponents.Core.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
