using System;
using System.Collections.Generic;
using System.Text;

namespace ShopComponents.Core.DTOs;

public class VentaDto
{
    public int Id { get; set; }
    public DateTime? Fecha { get; set; }
    public decimal Total { get; set; }
    public int? ClienteId { get; set; }
    public int? UsuarioId { get; set; }
}